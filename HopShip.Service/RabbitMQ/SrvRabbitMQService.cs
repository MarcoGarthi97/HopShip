using AutoMapper;
using HopShip.Data.DTO.RabbitMQ;
using HopShip.Data.Enum;
using HopShip.Library.RabbitMQ;
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
        public Task ToEnqueueAsync(EnumQueueRabbit enumQueueRabbit);
        public Task<QueueMessageRabbitMQ?> GetMessageAsync(EnumQueueRabbit enumQueueRabbit);
    }

    public class SrvRabbitMQService : ISrvRabbitMQService
    {
        private readonly ILogger<SrvRabbitMQService> _logger;
        private readonly IMapper _mapper;
        private readonly IFactoryRabbitMQ _factoryRabbitMQ;
        private IModel? _channel;

        public SrvRabbitMQService(ILogger<SrvRabbitMQService> logger, IMapper mapper, IFactoryRabbitMQ factoryRabbitMQ)
        {
            _logger = logger;
            _mapper = mapper;
            _factoryRabbitMQ = factoryRabbitMQ;
        }

        public async Task ToEnqueueAsync(EnumQueueRabbit enumQueueRabbit, QueueMessageRabbitMQ queueMessageRabbitMQ)
        {
            _logger.LogInformation("Start ToEnqueueAsync");

            await GetQueueDeclareAsync(enumQueueRabbit);

            string json = JsonSerializer.Serialize(queueMessageRabbitMQ);

            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: "",
                                    routingKey: GetNameQueue(enumQueueRabbit),
                                    basicProperties: null,
                                    body: body);

            _logger.LogInformation("End ToEnqueueAsync");
        }

        public async Task<QueueMessageRabbitMQ?> GetMessageAsync(EnumQueueRabbit enumQueueRabbit)
        {
            _logger.LogInformation("Start GetMessage");

            await GetQueueDeclareAsync(enumQueueRabbit);

            var tcs = new TaskCompletionSource<string>();
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            cts.Token.Register(() =>
            {
                tcs.TrySetResult(string.Empty);

                _logger.LogInformation("Timeout reached while waiting message");
            });

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, x) =>
            {
                var body = x.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);

                tcs.TrySetResult(message);
            };

            string consumerTag = _channel.BasicConsume(queue: GetNameQueue(enumQueueRabbit), autoAck: true, consumer: consumer);

            string message = await tcs.Task;
            _channel.BasicCancel(consumerTag);

            if (!string.IsNullOrEmpty(message))
            {
                var messageRabbitMQ = JsonSerializer.Deserialize<QueueMessageRabbitMQ>(message);

                _logger.LogInformation("End GetMessage");

                return messageRabbitMQ;
            }
            else
            {
                _logger.LogInformation("End GetMessage null");

                return null;
            }
        }

        private async Task GetQueueDeclareAsync(EnumQueueRabbit enumQueueRabbit)
        {
            string nameQueue = GetNameQueue(enumQueueRabbit);

            await GetChannelAsync();
            _channel.QueueDeclare(queue: nameQueue,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
        }

        private async Task GetChannelAsync()
        {
            _channel = await _factoryRabbitMQ.GetChannelAsync();
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
    }
}
