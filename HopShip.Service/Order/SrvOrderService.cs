using AutoMapper;
using HopShip.Data.DTO.Service;
using HopShip.Data.Enum;
using HopShip.Repository.Order;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Service.Order
{
    public interface ISrvOrderService
    {
        public Task<IEnumerable<SrvOrder>> GetOrders();
    }

    public class SrvOrderService : ISrvOrderService
    {
        private readonly ILogger<SrvOrderService> _logger;
        private readonly IMapper _mapper;
        private readonly IMdlOrderRepository _repository;

        public SrvOrderService(ILogger<SrvOrderService> logger, IMdlOrderRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SrvOrder>> GetOrders()
        {
            _logger.LogInformation("Start GetOrders");

            var mdlOrders = await _repository.FindAsync(x => x.Status == EnumStatusOrder.OrderCreated);
            var srvOrders = _mapper.Map<IEnumerable<SrvOrder>>(mdlOrders);

            _logger.LogInformation("End GetOrders");

            return srvOrders;
        }
    }
}
