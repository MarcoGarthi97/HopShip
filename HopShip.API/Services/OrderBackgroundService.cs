using HopShip.Data.DTO.Service;
using HopShip.Library.BackgroundService;
using HopShip.Service.Order;
using HopShip.Service.RabbitMQ;

namespace HopShip.API.Services
{
    public class OrderBackgroundService : IstanceBackgroundService
    {
        private IServiceProvider _serviceProvider;
        public OrderBackgroundService(
        ILogger<OrderBackgroundService> logger,
        IConfiguration configuration,
        IServiceProvider serviceProvider)
        : base(logger, configuration) 
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteServiceAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Start ExecuteServiceAsync");

                try
                {
                    using(var scope = _serviceProvider.CreateScope())
                    {
                        var rabbitMQService = scope.ServiceProvider.GetRequiredService<ISrvRabbitMQService>();
                        var rabbitMessage = await rabbitMQService.GetMessageAsync(Data.Enum.EnumQueueRabbit.OrderService);

                        if(rabbitMessage != null)
                        {
                            var orderService = scope.ServiceProvider.GetRequiredService<ISrvOrderService>();
                            //IEnumerable<SrvOrder> orders = await service.GetOrdersAsync();
                            //if(orders.Any())
                            //{

                            //}

                            //faccio cose
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }

                _logger.LogInformation("End ExecuteServiceAsync");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
