using Core.Common.Outbox;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Outbox.Stores.MongoDb
{
    public static class MongoDbOutboxExtensions
    {
        public static IServiceCollection AddMongoDbOutbox(this IServiceCollection services, IConfiguration Configuration)
        {
            var options = new MongoDbOutboxOptions();
            Configuration.GetSection(nameof(OutboxOptions)).Bind(options);
            services.Configure<MongoDbOutboxOptions>(Configuration.GetSection(nameof(OutboxOptions)));

            services.AddScoped<IOutboxStore, MongoDbOutboxStore>();

            return services;
        }
    }
}
