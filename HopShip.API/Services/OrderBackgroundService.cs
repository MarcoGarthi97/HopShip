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
        private readonly IServiceProvider _serviceProvider;
        private ISrvRabbitMQService _rabbitService;
        private ISrvOrderService _orderService;
        private ISrvOrderProductService _orderProductService;
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

                var scope = _serviceProvider.CreateScope();
                try
                {
                    _rabbitService = scope.ServiceProvider.GetRequiredService<ISrvRabbitMQService>();
                    _orderService = scope.ServiceProvider.GetRequiredService<ISrvOrderService>();
                    _orderProductService = scope.ServiceProvider.GetRequiredService<ISrvOrderProductService>();

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
                    scope.Dispose();
                }

                _logger.LogInformation("End ExecuteServiceAsync");
            }
        }

        private async Task SubscriptionModeAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start SubscriptionModeAsync");

            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            await _rabbitService.SubscribeAsync(EnumQueueRabbit.OrderService, async (message) => await ProcessOrderMessageAsync(message, _cancellationTokenSource.Token), _cancellationTokenSource.Token);

            try
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("Subscribe expired");
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
                    await _rabbitService.ProcessMessageAsync(
                            EnumQueueRabbit.OrderService,
                            async (message) => await ProcessOrderMessageAsync(message, stoppingToken),
                            _batchSize,
                            stoppingToken);
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

        private async Task ProcessOrderMessageAsync(QueueMessageRabbitMQ message, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start ProcessOrderMessageAsync");

                var order = await _orderService.GetOrderAsync(message.Id, EnumStatusOrder.OrderCreated, cancellationToken);

                var statusOrder = await ProcessOrderAsync(order, cancellationToken);
                if (statusOrder == EnumStatusOrder.OrderValidated)
                {
                    QueueMessageRabbitMQ messageforQueue = new QueueMessageRabbitMQ(order.Id);

                    await _rabbitService.EnqueueMessageAsync(EnumQueueRabbit.PaymentService, messageforQueue);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            _logger.LogInformation("End ProcessOrderMessageAsync");
        }

        private async Task<EnumStatusOrder> ProcessOrderAsync(SrvOrder order, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start ProcessOrderAsync");

                var orderProducts = await _orderProductService.GerOrderProductsByOrderIdAsync(order.Id, cancellationToken);

                var resultCheck = _orderProductService.CheckOrderProductsAfter(orderProducts, cancellationToken);
                order.Status = resultCheck;

                await _orderService.UpdateOrdersStatusAsync(order, cancellationToken);

                return resultCheck;

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
