using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace HopShip.Library.ServicesCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedLibraryServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses()
            .AsImplementedInterfaces(typeInfo =>
                typeInfo.GetInterfaces().Any(i =>
                    i == typeof(IForServiceCollectionExtension) ||
                    typeInfo == typeof(IForServiceCollectionExtension))
            )
            .WithScopedLifetime()
            );

            return services;
        }
    }
}
