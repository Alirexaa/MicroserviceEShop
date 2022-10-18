using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Catalog.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Catalog.Infrastructure.Data
{
    public static class ServiceCollectionExtenstions
    {
        public static IServiceCollection AddCatalogDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(c =>
            {
                c.UseNpgsql(configuration["PostgresOptions:ConnectionString"], opt =>
                { 
                    opt.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName);
                    opt.EnableRetryOnFailure(maxRetryCount:15,maxRetryDelay:TimeSpan.FromSeconds(10),null);
                });
            });
            services.AddScoped<IUnitOfWork, CatalogDbContext>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(BaseRepository<>));
            return services;
        }
    }
}
