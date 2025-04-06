using HopShip.Data.DTO.RabbitMQ;
using HopShip.Data.DTO.Service;
using HopShip.Data.Enum;
using HopShip.Library.BackgroundService;
using HopShip.Service.Order;
using HopShip.Service.OrderProduct;
using HopShip.Service.Payment;
using HopShip.Service.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace HopShip.API.Services
{
    public class PaymentBackgroundService : IstanceBackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        private ISrvRabbitMQService _rabbitService;
        private ISrvPaymentService _paymentService;
        private readonly int _processInterval;
        private readonly int _batchSize;
        private readonly bool _useSubscriptionMode;

        private CancellationTokenSource _cancellationTokenSource;

        public PaymentBackgroundService(ILogger<IstanceBackgroundService> logger, IConfiguration configuration, IServiceProvider serviceProvider) : base(logger, configuration)
        {
            _serviceProvider = serviceProvider;
            _processInterval = configuration.GetValue<int>("Develop:RabbitMQ:ProcessInterval", 10);
            _batchSize = configuration.GetValue<int>("Develop:RabbitMQ:Batchsize", 10);
            _useSubscriptionMode = configuration.GetValue<bool>("Develop:RabbitMQ:UseSubscriptionMode", true);
        }

        protected override async Task ExecuteServiceAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Start ExecuteServiceAsync");

                var scope = _serviceProvider.CreateScope();

                try
                {
                    _rabbitService = scope.ServiceProvider.GetRequiredService<ISrvRabbitMQService>();
                    _paymentService = scope.ServiceProvider.GetRequiredService<ISrvPaymentService>();

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

            await _rabbitService.SubscribeAsync(EnumQueueRabbit.PaymentService, async (message) => await ProcessPaymentMessageAsync(message, _cancellationTokenSource.Token), _cancellationTokenSource.Token);

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
                            EnumQueueRabbit.PaymentService,
                            async (message) => await ProcessPaymentMessageAsync(message, stoppingToken),
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

        private async Task ProcessPaymentMessageAsync(QueueMessageRabbitMQ message, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start ProcessOrderMessageAsync");

                SrvPayment srvPayment = await _paymentService.GetPaymentByOrderIdAsync(message.Id, cancellationToken);
                srvPayment.PaymentStatus = EnumStatusPayment.Processing;

                await _paymentService.UpdatePaymentAsync(srvPayment, cancellationToken);

                srvPayment.PaymentStatus = await _paymentService.CheckPaymentAsync(srvPayment, cancellationToken);
                await _paymentService.UpdatePaymentAsync(srvPayment, cancellationToken);

                if (srvPayment.PaymentStatus == EnumStatusPayment.Completed)
                {
                    QueueMessageRabbitMQ queueMessageRabbitMQ = new() { Id = message.Id };

                    await _rabbitService.EnqueueMessageAsync(EnumQueueRabbit.ShippingService, queueMessageRabbitMQ);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            _logger.LogInformation("End ProcessOrderMessageAsync");
        }
    }
}
