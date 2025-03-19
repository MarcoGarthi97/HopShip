using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HopShip.Library.RabbitMQ
{
    public interface IFactoryRabbitMQ
    {
        public Task<IModel> GetChannelAsync();
        public Task CloseAsync();
    }

    public class FactoryRabbitMQ : IFactoryRabbitMQ
    {
        private readonly ILogger<FactoryRabbitMQ> _logger;
        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1);
        private readonly ConnectionFactory _connectionFactory;
        private IModel _channel;
        private IConnection _connection;
        private bool _disposed;

        public FactoryRabbitMQ (ILogger<FactoryRabbitMQ> logger)
        {
            _logger = logger;
        }

        public async Task<IModel> GetChannelAsync()
        {
            _logger.LogInformation("Start GetChannelAsync");

            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(FactoryRabbitMQ));
            }

            await _connectionLock.WaitAsync();

            try
            {
                if(_channel != null && !_channel.IsOpen)
                {
                    _logger.LogWarning("Channel exists but is closed. Recreating channel");
                    _channel = null;
                }

                if (_channel == null)
                {
                    if(_connection != null && !_connection.IsOpen)
                    {
                        await CloseConnectionAsync();
                    }

                    if(_connection == null)
                    {
                        _logger.LogInformation("Create channel");

                        ConnectionFactory connectionFactory = new ConnectionFactory
                        {
                            HostName = "rabbitmq",
                            Port = 5672,
                            UserName = "guest",
                            Password = "guest",
                            DispatchConsumersAsync = true,
                            AutomaticRecoveryEnabled = true,
                            NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                            RequestedHeartbeat = TimeSpan.FromSeconds(60),
                            ClientProvidedName = "HopShip"
                        };

                        try
                        {
                            _connection = await Task.Run(() => connectionFactory.CreateConnection());
                            _logger.LogInformation("Connection established successfully");
                        }
                        catch (BrokerUnreachableException ex)
                        {
                            _logger.LogError(ex, ex.Message);
                            throw;
                        }
                    }

                    try
                    {
                        _channel = await Task.Run(() => _connection.CreateModel());
                        _logger.LogInformation("Channel created successfully");
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }

                    _logger.LogInformation("End GetChannelAsync");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            finally
            {
                _connectionLock.Release();
            }

            return _channel;
        }

        public async Task CloseAsync()
        {
            await _connectionLock.WaitAsync();

            try
            {
                await CloseConnectionAsync();
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        private async Task CloseConnectionAsync()
        {
            try
            {
                if(_channel != null && _channel.IsOpen)
                {
                    await Task.Run(() => _channel.Close());
                    _logger.LogInformation("Channel closed successfully");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error while closing channel");
            }
            finally
            {
                _channel = null;
            }

            try
            {
                if (_connection != null && _connection.IsOpen)
                {
                    await Task.Run(() => _connection.Close());
                    _logger.LogInformation("Connection closed successfully");
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error while closing connection");
            }
            finally
            {
                _connection = null;
            }
        }
    }
}
