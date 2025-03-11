using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace HopShip.Library.BackgroundService
{
    public static class ServiceCollectionExtensions
    {
        //Ricordasi di passare sempre l'assembly sennò non si riesce a prendere i servizi
        public static IServiceCollection AddBackgroundWorkers(this IServiceCollection services, Assembly assembly = null)
        {
            assembly ??= Assembly.GetExecutingAssembly();

            var backgroundServiceTypes = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && typeof(IstanceBackgroundService).IsAssignableFrom(x) && x != typeof(IstanceBackgroundService));

            foreach(var service in backgroundServiceTypes)
            {
                services.Add(new ServiceDescriptor(
                typeof(IHostedService),
                service,
                ServiceLifetime.Singleton));
            }

            return services;
        }
    }
}
