using Core.Common.Cqrs.Commands;
using Core.Common.Cqrs.Commands.Dispatcher;
using Core.Common.Cqrs.Queris;
using Core.Common.Cqrs.Queris.Dispatcher;
using Core.Common.Event.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Common.Event
{
    public static class Extensions
    {
        public static IServiceCollection AddEventHandler(this IServiceCollection services)
        {
            services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>))
            ).AsImplementedInterfaces()
            .WithTransientLifetime());
            return services;
        }

        public static IServiceCollection AddInMemoryEventDispatcher(this IServiceCollection services)
        {
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            return services;
        }
    }
}
