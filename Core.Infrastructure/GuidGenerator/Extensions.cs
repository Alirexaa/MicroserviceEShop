using Core.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.GuidGenerator
{
    public static class Extensions
    {
        public static IServiceCollection AddSequentialGuidGenerator(this IServiceCollection services)
        {
            services.AddSequentialGuidGenerator(SequentialGuidType.SequentialAtEnd);
            return services;
        }
        public static IServiceCollection AddSequentialGuidGenerator(this IServiceCollection services,SequentialGuidType sequentialGuidType)
        {
            services.Configure<SequentialGuidGeneratorOptions>(options =>
            {
                options.DefaultSequentialGuidType = sequentialGuidType;
            });
            services.AddTransient<IGuidGenerator, ApplicationSequentialGuidGenerator>();
            return services;
        }
    }
}
