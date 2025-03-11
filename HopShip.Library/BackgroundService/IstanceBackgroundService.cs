using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HopShip.Library.BackgroundService
{
    public abstract class IstanceBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
    {
        protected readonly ILogger<IstanceBackgroundService> _logger;
        protected readonly string _serviceName;
        protected readonly bool _isEnabled;

        protected IstanceBackgroundService(ILogger<IstanceBackgroundService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _serviceName = this.GetType().Name;

            // Leggiamo la configurazione dall'appsettings.json
            _isEnabled = configuration.GetValue<bool>($"Services:{_serviceName}", false);

            _logger.LogInformation($"Service {_serviceName} is {(_isEnabled ? "enabled" : "disabled")}");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_isEnabled)
            {
                _logger.LogInformation($"{_serviceName} is disabled. Skipping execution.");
                return;
            }

            _logger.LogInformation($"{_serviceName} is starting.");

            try
            {
                await ExecuteServiceAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred in {_serviceName}.");
            }
        }

        // Metodo astratto che le classi derivate dovranno implementare
        protected abstract Task ExecuteServiceAsync(CancellationToken stoppingToken);
    }
}
