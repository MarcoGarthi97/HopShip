using AutoMapper;
using HopShip.Data.DTO.Repository;
using HopShip.Data.DTO.Service;
using HopShip.Data.Enum;
using HopShip.Repository.Order;
using HopShip.Service.Product;
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
        public Task<IEnumerable<SrvOrder>> GetOrdersAsync(CancellationToken cancellationToken);
        public Task<SrvOrder> InsertOrdersAsync(SrvOrder srvOrder, CancellationToken cancellationToken);
    }

    public class SrvOrderService : ISrvOrderService
    {
        private readonly ILogger<SrvOrderService> _logger;
        private readonly IMapper _mapper;
        private readonly IMdlOrderRepository _repositoryOrder;

        public SrvOrderService(ILogger<SrvOrderService> logger, IMdlOrderRepository mdlOrderRepository, IMapper mapper)
        {
            _logger = logger;
            _repositoryOrder = mdlOrderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SrvOrder>> GetOrdersAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start GetOrdersAsync");

            IEnumerable<MdlOrder> mdlOrders = await _repositoryOrder.FindAsync(x => x.Status == EnumStatusOrder.OrderCreated, cancellationToken);
            IEnumerable<SrvOrder> srvOrders = _mapper.Map<IEnumerable<SrvOrder>>(mdlOrders);

            _logger.LogInformation("End GetOrdersAsync");

            return srvOrders;
        }

        public async Task<SrvOrder> InsertOrdersAsync(SrvOrder srvOrder, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start InsertOrdersAsync");

            MdlOrder mdlOrder = _mapper.Map<MdlOrder>(srvOrder);
            mdlOrder.CreateDate = DateTime.UtcNow;
            mdlOrder.Status = EnumStatusOrder.OrderCreated;

            mdlOrder = await _repositoryOrder.InsertAsync(mdlOrder, cancellationToken);
            srvOrder = _mapper.Map<SrvOrder>(mdlOrder);

            _logger.LogInformation("End InsertOrdersAsync");

            return srvOrder;
        }

        
    }
}
