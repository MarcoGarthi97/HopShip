using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace HopShip.Library.RabbitMQ
{
    public interface IFactoryRabbitMQ
    {
        public Task<IModel> GetChannelAsync();
    }

    public class FactoryRabbitMQ : IFactoryRabbitMQ
    {
        private readonly ILogger<FactoryRabbitMQ> _logger;
        private IModel _channel;
        public FactoryRabbitMQ (ILogger<FactoryRabbitMQ> logger)
        {
            _logger = logger;
        }

        public async Task<IModel> GetChannelAsync()
        {
            _logger.LogInformation("Start GetChannelAsync");

            if(_channel == null)
            {
                _logger.LogInformation("Create channel");

                ConnectionFactory connectionFactory = new ConnectionFactory
                {
                    HostName = "rabbitmq",
                    Port = 5672,
                    UserName = "guest",
                    Password = "guest",
                    DispatchConsumersAsync = true,
                };

                IConnection connection = await Task.Run(() => connectionFactory.CreateConnection());

                _channel = await Task.Run(() => connection.CreateModel());
            }

            _logger.LogInformation("End GetChannelAsync");

            return _channel;
        }
    }
}
