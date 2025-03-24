using HopShip.Data.DTO.RabbitMQ;
using HopShip.Data.DTO.Service;
using HopShip.Data.Enum;
using HopShip.Library.BackgroundService;
using HopShip.Service.Order;
using HopShip.Service.OrderProduct;
using HopShip.Service.RabbitMQ;

namespace HopShip.API.Services
{
    public class OrderBackgroundService : IstanceBackgroundService
    {
        private IServiceProvider _serviceProvider;
        private readonly int _processInterval;
        private readonly int _batchSize;
        private readonly bool _useSubscriptionMode;
        private CancellationTokenSource _cancellationTokenSource;
        public OrderBackgroundService(
        ILogger<OrderBackgroundService> logger,
        IConfiguration configuration,
        IServiceProvider serviceProvider)
        : base(logger, configuration) 
        {
            _serviceProvider = serviceProvider;
            _processInterval = configuration.GetValue<int>("Develop:RabbitMQ:ProcessInterval", 10);
            _batchSize = configuration.GetValue<int>("Develop:RabbitMQ:Batchsize", 10);
            _useSubscriptionMode= configuration.GetValue<bool>("Develop:RabbitMQ:UseSubscriptionMode", true);
        }

        protected override async Task ExecuteServiceAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Start ExecuteServiceAsync");

                try
                {
                    if (_useSubscriptionMode)
                    {
                        await SubscriptionModeAsync(stoppingToken);
                    }
                    else
                    {
                        await PollingModeAsync(stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
                finally
                {
                    _cancellationTokenSource?.Cancel();
                    _cancellationTokenSource?.Dispose();
                    _cancellationTokenSource = null;
                }

                _logger.LogInformation("End ExecuteServiceAsync");
            }
        }

        private async Task SubscriptionModeAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start SubscriptionModeAsync");

            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            using (var scope = _serviceProvider.CreateScope())
            {
                var rabbitMQService = scope.ServiceProvider.GetRequiredService<ISrvRabbitMQService>();

                await rabbitMQService.SubscribeAsync(EnumQueueRabbit.OrderService, async (message) => await ProcessOrderMessageAsync(message, rabbitMQService, _cancellationTokenSource.Token), _cancellationTokenSource.Token);

                try
                {
                    await Task.Delay(Timeout.Infinite, cancellationToken);
                }
                catch(TaskCanceledException)
                {
                    _logger.LogInformation("Subscribe expired");
                }
            }

            _logger.LogInformation("End SubscriptionModeAsync");
        }

        private async Task PollingModeAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start PollingModeAsync");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Polling");

                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var rabbitMQService = scope.ServiceProvider.GetRequiredService<ISrvRabbitMQService>();

                        await rabbitMQService.ProcessMessageAsync(
                            EnumQueueRabbit.OrderService,
                            async (message) => await ProcessOrderMessageAsync(message, rabbitMQService, stoppingToken),
                            _batchSize,
                            stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }

                _logger.LogInformation("End PollingModeAsync");

                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(_processInterval), stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
        }

        private async Task ProcessOrderMessageAsync(QueueMessageRabbitMQ message, ISrvRabbitMQService rabbitMQService, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start ProcessOrderMessageAsync");

                using(var scope = _serviceProvider.CreateScope())
                {
                    var orderService = scope.ServiceProvider.GetRequiredService<ISrvOrderService>();
                    var orders = await orderService.GetOrdersAsync(new int[] { message.Id }, cancellationToken);

                    foreach(var order in orders)
                    {
                        var statusOrder = await ProcessOrderAsync(order, orderService, cancellationToken);
                        if(statusOrder == EnumStatusOrder.OrderValidated)
                        {
                            QueueMessageRabbitMQ messageforQueue = new QueueMessageRabbitMQ(order.Id);

                            await rabbitMQService.EnqueueMessageAsync(EnumQueueRabbit.PaymentService, messageforQueue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            _logger.LogInformation("End ProcessOrderMessageAsync");
        }

        private async Task<EnumStatusOrder> ProcessOrderAsync(SrvOrder order, ISrvOrderService orderService, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start ProcessOrderAsync");

                using(var scope = _serviceProvider.CreateScope())
                {
                    var orderProductService = scope.ServiceProvider.GetRequiredService<ISrvOrderProductService>();
                    var orderProducts = await orderProductService.GerOrderProductsByOrderIdAsync(order.Id, cancellationToken);

                    var resultCheck = orderProductService.CheckOrderProductsAfter(orderProducts, cancellationToken);
                    order.Status = resultCheck;

                    await orderService.UpdateOrdersStatusAsync(order, cancellationToken);

                    return resultCheck;
                }

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            _logger.LogInformation("End ProcessOrderAsync");

            return EnumStatusOrder.OrderFailed;
        }
    }
}
