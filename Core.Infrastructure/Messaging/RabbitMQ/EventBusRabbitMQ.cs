using Core.Common.Extenstions;
using Core.Common.Messaging;
using Core.Domain;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Core.Infrastructure.Messaging.RabbitMQ
{
    internal class EventBusRabbitMQ : IEventListener
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly IConventionsBuilder _conventionsBuilder;
        private readonly IRabbitMqSerializer _serializer;
        public EventBusRabbitMQ(IRabbitMQPersistentConnection connection, ILogger<EventBusRabbitMQ> logger, IConventionsBuilder conventionsBuilder, IRabbitMqSerializer serializer)
        {
            _persistentConnection = connection;
            _logger = logger;
            _conventionsBuilder = conventionsBuilder;
            _serializer = serializer;
        }
        public Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {

            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(retryCount: 5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event, $"{time.TotalSeconds:n1}", ex.Message);
                });

            var eventName = MessageBrokersHelper.GetTypeName(@event.GetType());

            _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event, eventName);

            using var channel = _persistentConnection.CreateModel();
            _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event);




            var exchange = _conventionsBuilder.GetExchange(typeof(TEvent));
            var routingKey = _conventionsBuilder.GetRoutingKey(typeof(TEvent));
            var queue = _conventionsBuilder.GetQueue(typeof(TEvent));



            channel.ExchangeDeclare(exchange: exchange, type: "topic");
            channel.QueueDeclare(queue, exclusive: false, autoDelete: false);
            channel.QueueBind(queue, exchange, routingKey);




            policy.Execute(() =>
            {
                var body = _serializer.Serialize(@event);
                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2; // persistent

                _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event);

                channel.BasicPublish(
                    exchange: exchange,
                    routingKey: routingKey,
                    basicProperties: properties,
                    body: body.ToArray());
            });

            return Task.CompletedTask;

        }


        public void Subscribe(Type type)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var channel = _persistentConnection.CreateModel();
            //var eventName = MessageBrokersHelper.GetTypeName(type);
            //var queneName = AppDomain.CurrentDomain.FriendlyName.Trim().Trim('_') + "_" + eventName;


            //channel.QueueDeclare(convention.Queue, exclusive: false, autoDelete: false);
            //channel.ExchangeDeclare(exchange: convention.Exchage, type: "topic");
            //channel.QueueBind(convention.Queue, convention.Exchage, convention.RoutingKey);


            var queue = _conventionsBuilder.GetQueue(type);


            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var message = _serializer.Deserialize(eventArgs.Body.Span);
                Console.WriteLine(message);

                //channel.BasicAck(eventArgs.DeliveryTag, false);
            };
            channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
        }

        public void Subscribe<TEvent>() where TEvent : IEvent
        {
            Subscribe(typeof(TEvent));
        }
    }
}
