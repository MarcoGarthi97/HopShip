using AutoMapper;
using HopShip.Data.DTO.RabbitMQ;
using HopShip.Data.Enum;
using HopShip.Library.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HopShip.Service.RabbitMQ
{
    public interface ISrvRabbitMQService
    {
        Task EnqueueMessageAsync(EnumQueueRabbit queueType, QueueMessageRabbitMQ message);
        Task<QueueMessageRabbitMQ> GetMessageAsync(EnumQueueRabbit queueType, int timeoutSeconds = 30);
        Task ProcessMessageAsync(EnumQueueRabbit queueType, Func<QueueMessageRabbitMQ, Task> messageHandler, int batchSize = 10, CancellationToken cancellationToken = default);
        Task SubscribeAsync(EnumQueueRabbit queueType, Func<QueueMessageRabbitMQ, Task> messageHandler, CancellationToken cancellationToken = default);
    }

    public class SrvRabbitMQService : ISrvRabbitMQService
    {
        private readonly ILogger<SrvRabbitMQService> _logger;
        private readonly IFactoryRabbitMQ _factoryRabbitMQ;
        private IModel? _channel;
        private bool _disposed = false;
        private Dictionary<string, string> _activeConsumers = new Dictionary<string, string>();

        public SrvRabbitMQService(ILogger<SrvRabbitMQService> logger, IFactoryRabbitMQ factoryRabbitMQ)
        {
            _logger = logger;
            _factoryRabbitMQ = factoryRabbitMQ;
        }

        public async Task EnqueueMessageAsync(EnumQueueRabbit queueType, QueueMessageRabbitMQ message)
        {
            _logger.LogInformation("Start EnqueueMessageAsync");

            await EnsureQueueDeclaredAsync(queueType);

            string json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            _channel!.BasicPublish(exchange: "", routingKey: GetNameQueue(queueType), basicProperties: null, body: body);

            _logger.LogInformation("End EnqueueMessageAsync");
        } 

        public async Task<QueueMessageRabbitMQ> GetMessageAsync(EnumQueueRabbit queueType, int timeoutSeconds = 30)
        {
            _logger.LogInformation("Start GetMessageAsync");

            await EnsureQueueDeclaredAsync(queueType);

            var tcs = new TaskCompletionSource<string>();
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));

            cts.Token.Register(() =>
            {
                tcs.TrySetResult(string.Empty);
                _logger.LogInformation("Timeout for waiting messages for queue: " + GetNameQueue(queueType));
            });

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, x) =>
            {
                var body = x.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                tcs.TrySetResult(message);
            };

            string consumerTag = _channel!.BasicConsume(queue: GetNameQueue(queueType), autoAck: true, consumer: consumer);

            string message = await tcs.Task;
            _channel!.BasicCancel(consumerTag);

            if(!string.IsNullOrEmpty(message))
            {
                try
                {
                    var messageRabbitMQ = JsonSerializer.Deserialize<QueueMessageRabbitMQ>(message);

                    _logger.LogInformation("Message received");

                    return messageRabbitMQ!;
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }

            _logger.LogInformation("End GetMessageAsync");

            return null!;
        }

        public async Task ProcessMessageAsync(EnumQueueRabbit queueType, Func<QueueMessageRabbitMQ, Task> messageHandler, int batchSize = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Start ProcessMessageAsync");

            await EnsureQueueDeclaredAsync(queueType);

            _channel!.BasicQos(prefetchSize: 0, prefetchCount: (ushort)batchSize, global: false);

            int processedCount = 0;

            while(processedCount < batchSize && !cancellationToken.IsCancellationRequested)
            {
                var result = _channel!.BasicGet(GetNameQueue(queueType), false);

                if(result == null)
                {
                    _logger.LogInformation("There isn't message in queue: " + GetNameQueue(queueType) + " messages processed: " + processedCount);
                    break;
                }

                var body = result.Body.ToArray();
                await BasicAck(body, messageHandler, result.DeliveryTag, cancellationToken);               
            }

            _logger.LogInformation("End ProcessMessageAsync");
        }

        public async Task SubscribeAsync(EnumQueueRabbit queueType, Func<QueueMessageRabbitMQ, Task> messageHandler, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Start SubscribeAsync");

            await EnsureQueueDeclaredAsync(queueType);

            _channel!.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, x) =>
            {
                var body = x.Body.ToArray();
                await BasicAck(body, messageHandler, x.DeliveryTag, cancellationToken);
            };

            string consumerTag = _channel!.BasicConsume(queue: GetNameQueue(queueType), autoAck: false, consumer: consumer);

            _activeConsumers[GetNameQueue(queueType)] = consumerTag;

            cancellationToken.Register(() =>
            {
                try
                {
                    if (_channel != null && _channel.IsOpen && _activeConsumers.TryGetValue(GetNameQueue(queueType), out string? tag))
                    {
                        _channel.BasicCancel(tag);
                        _activeConsumers.Remove(GetNameQueue(queueType));

                        _logger.LogInformation("Consumer delete from queue: " + GetNameQueue(queueType));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            });

            _logger.LogInformation("End SubscribeAsync");
        }

        private async Task BasicAck(byte[] body, Func<QueueMessageRabbitMQ, Task> messageHandler, ulong deliveryTag, CancellationToken cancellationToken)
        {
            try
            {
                string messageJson = Encoding.UTF8.GetString(body);

                if (!string.IsNullOrEmpty(messageJson))
                {
                    var message = JsonSerializer.Deserialize<QueueMessageRabbitMQ>(messageJson);

                    if (message != null)
                    {
                        await messageHandler(message);
                        _channel!.BasicAck(deliveryTag, false);
                    }
                    else
                    {
                        _logger.LogWarning("Impossible convert json");
                        _channel!.BasicNack(deliveryTag, false, true);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                _channel!.BasicNack(deliveryTag, false, true);
            }
        }

        private async Task EnsureQueueDeclaredAsync(EnumQueueRabbit enumQueueRabbit)
        {
            string nameQueue = GetNameQueue(enumQueueRabbit);

            await EnsureChannelCreatedAsync();
            _channel!.QueueDeclare(queue: nameQueue,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
        }

        private async Task EnsureChannelCreatedAsync()
        {
            if(_channel == null || !_channel.IsOpen)
            {
                _channel = await _factoryRabbitMQ.GetChannelAsync();
            }
        }

        private string GetNameQueue(EnumQueueRabbit enumQueueRabbit)
        {
            switch (enumQueueRabbit)
            {
                case EnumQueueRabbit.OrderService:
                    return "OrderService";
                case EnumQueueRabbit.PaymentService:
                    return "PaymentService";
                case EnumQueueRabbit.ShippingService:
                    return "ShippingService";
                default:
                    throw new Exception();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if(disposing)
                {
                    foreach(var consumer in _activeConsumers)
                    {
                        try
                        {
                            if(_channel != null && _channel.IsOpen)
                            {
                                _channel.BasicCancel(consumer.Value);
                            }
                        }
                        catch(Exception ex)
                        {
                            _logger.LogError(ex, "Error to delete for queue: " + consumer.Key + " message: " + ex.Message);
                        }
                    }

                    _activeConsumers.Clear();

                    _channel?.Close();
                    _channel?.Dispose();
                    _channel = null;
                }

                _disposed = true;
            }
        }
    }
}
