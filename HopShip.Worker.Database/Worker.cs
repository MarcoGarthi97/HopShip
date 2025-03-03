using HopShip.Worker.Database.Migrations;

namespace HopShip.Worker.Database
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMigrationOrchestrator _migrationOrchestrator;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IMigrationOrchestrator migrationOrchestrator, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _migrationOrchestrator = migrationOrchestrator;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var migration = scope.ServiceProvider.GetRequiredService<IMigrationOrchestrator>();

                    await migration.ExecuteAsync(stoppingToken);
                }
            }
        }
    }
}
