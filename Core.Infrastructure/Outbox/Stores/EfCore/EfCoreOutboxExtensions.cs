using Core.Common.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Infrastructure.Outbox.Stores.EfCore
{
    public static class EfCoreOutboxExtensions
    {
        public static IServiceCollection AddEfCoreOutboxStore(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptions)
        {
            services.AddDbContext<EfCoreOutboxContext>(dbContextOptions);
            services.AddScoped<IOutboxStore, EfCoreOutboxStore>();

            return services;
        }
    }
}
