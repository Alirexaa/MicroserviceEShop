using Core.Common.Cqrs.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Catalog.Infrastructure.Behaviours
{
    public static class ServiceCollectionExtenstions
    {
        public static IServiceCollection AddCommandBehaviours(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICommandPipelineBehavior<,>),typeof(TransactionBehaviour<,>));
            return services;
        }
    }
}
