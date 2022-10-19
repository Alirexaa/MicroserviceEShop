using Core.Common.Cqrs.Commands;
using Core.Common.Cqrs.Commands.Dispatcher;
using Core.Common.Cqrs.Queris;
using Core.Common.Cqrs.Queris.Dispatcher;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Common.Cqrs
{
    public static class Extensions
    {
        public static IServiceCollection AddCommandHandler(this IServiceCollection services,Assembly assemblyContainsHandler)
        {
            services.Scan(s =>
            s.FromAssemblies(assemblyContainsHandler)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>))
            ).AsImplementedInterfaces()
            .WithTransientLifetime());
            return services;
        }

        public static IServiceCollection AddInMemoryCommandDispatcher(this IServiceCollection services)
        {
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            return services;
        }

        public static IServiceCollection AddQueryHandler(this IServiceCollection services,Assembly assemblyContainsHandlers)
        {
            services.Scan(s =>
            s.FromAssemblies(assemblyContainsHandlers)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>))
            ).AsImplementedInterfaces()
            .WithTransientLifetime());
            return services;
        }

        public static IServiceCollection AddInMemoryQueryDispatcher(this IServiceCollection services)
        {
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            return services;
        }
    }
}
