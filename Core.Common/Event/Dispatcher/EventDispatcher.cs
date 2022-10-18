using Core.Common.Cqrs.Commands;
using Core.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Event.Dispatcher
{
    internal class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task PublishLocal<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
        {
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
            await Task.WhenAll(handlers.Select(handler => handler.HandelAsync(@event, cancellationToken)));
        }
    }
}
