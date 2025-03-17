using AutoMapper;
using HopShip.Data.Enum;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Service.RabbitMQ
{
    public interface ISrvRabbitMQService
    {

    }

    public class SrvRabbitMQService : ISrvRabbitMQService
    {
        private readonly ILogger<SrvRabbitMQService> _logger;
        private readonly IMapper _mapper;
        //private readonly IFactoryService _factoryService;
        private readonly IModel _channel;

        public SrvRabbitMQService(ILogger<SrvRabbitMQService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public Task<bool> ToEnqueueAsync(EnumQueueRabbit enumQueueRabbit)
        {
            throw new Exception();
        }
    }
}
