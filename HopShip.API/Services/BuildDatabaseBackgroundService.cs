using HopShip.Library.BackgroundService;
using HopShip.Service.Database;
using Microsoft.Extensions.Configuration;

namespace HopShip.API.Services
{
    public class BuildDatabaseBackgroundService : IstanceBackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public BuildDatabaseBackgroundService(ILogger<IstanceBackgroundService> _logger, IConfiguration _configuration, IServiceProvider serviceProvider) : base(_logger, _configuration)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteServiceAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start ExecuteServiceAsync");

            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var databaseService = scope.ServiceProvider.GetRequiredService<ISrvDatabaseService>();
                    await databaseService.BuildDatabaseAsync(stoppingToken);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            _logger.LogInformation("End ExecuteServiceAsync");
        }
    }
}
