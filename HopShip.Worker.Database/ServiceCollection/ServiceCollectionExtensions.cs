using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Worker.Database.ServicesCollection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            );

            return services;
        }
    }
}
