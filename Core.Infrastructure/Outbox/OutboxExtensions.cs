﻿using Core.Common.Outbox;
using Core.Infrastructure.Outbox.Stores.EfCore;
using Core.Infrastructure.Outbox.Stores.MongoDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Infrastructure.Outbox
{
    public static class OutboxExtensions
    {
        public static IServiceCollection AddOutbox(this IServiceCollection services, IConfiguration Configuration, Action<DbContextOptionsBuilder> dbContextOptions = null)
        {
            var options = new OutboxOptions();
            Configuration.GetSection(nameof(OutboxOptions)).Bind(options);
            services.Configure<OutboxOptions>(Configuration.GetSection(nameof(OutboxOptions)));

            switch (options.OutboxType.ToLowerInvariant())
            {
                case "efcore":
                case "ef":
                    services.AddEfCoreOutboxStore(dbContextOptions);
                    break;
                case "mongo":
                case "mongodb":
                    services.AddMongoDbOutbox(Configuration);
                    break;
                default:
                    throw new Exception($"Outbox type '{options.OutboxType}' is not supported");
            }

            services.AddScoped<IOutboxListener, OutboxListener>();
            services.AddHostedService<OutboxProcessor>();

            return services;
        }
    }
}
