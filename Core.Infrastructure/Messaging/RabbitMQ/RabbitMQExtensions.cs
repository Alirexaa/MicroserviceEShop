using Core.Common.Messaging;
using Core.Domain;
using Core.Infrastructure.Messaging.RabbitMQ;
using Core.Infrastructure.Messaging.RabbitMQ.Conventions;
using Core.Infrastructure.Messaging.RabbitMQ.Serializers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Instantiation;
using System.Reflection;

namespace Infrastructure.MessageBrokers.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                //var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    DispatchConsumersAsync = true
                };

                factory.UserName = "guest";
                factory.Password = "guest";


                var retryCount = 5;
                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });
            services.AddSingleton<IConventionsBuilder, ConventionsBuilder>();
           services.AddSingleton<IRabbitMqSerializer, SystemTextJsonJsonRabbitMqSerializer>();

            services.AddSingleton<IEventListener, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var conventionsBuilder = sp.GetRequiredService<IConventionsBuilder>();
                var serializer = sp.GetRequiredService<IRabbitMqSerializer>();


                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();

                var retryCount = 5;

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, conventionsBuilder, serializer);
            });

            return services;
        }

        public static IApplicationBuilder UseSubscribeEvent<TEvent>(this IApplicationBuilder app) where TEvent : IEvent
        {
            app.ApplicationServices.GetRequiredService<IEventListener>().Subscribe<TEvent>();
            return app;
        }
        public static IApplicationBuilder UseSubscribeEvent(this IApplicationBuilder app,Type type) 
        {
            app.ApplicationServices.GetRequiredService<IEventListener>().Subscribe(type);
            return app;
        }

        public static IApplicationBuilder UseSubscribeAllEvents(this IApplicationBuilder app,Assembly assembly)
        {
            var types = assembly.GetTypes()
            .Where(mytype => mytype.GetInterfaces().Contains(typeof(IEvent)));

            foreach (var type in types)
            {
                app.UseSubscribeEvent(type);
            }
            return app;
        }
    }
}
