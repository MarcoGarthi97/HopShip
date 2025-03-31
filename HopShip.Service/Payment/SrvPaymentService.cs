using AutoMapper;
using HopShip.Data.DTO.Repository;
using HopShip.Data.DTO.Service;
using HopShip.Data.Enum;
using HopShip.Repository.Payment;
using HopShip.Service.Order;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Service.Payment
{
    public interface ISrvPaymentService
    {
        public Task<SrvPayment> GetPaymentByOrderIdAsync(int orderId, CancellationToken cancellationToken);
        public Task InsertPaymentAsync(SrvPayment srvPayment, CancellationToken cancellationToken);
        public Task UpdatePaymentAsync(SrvPayment srvPayment, CancellationToken cancellationToken);
        public Task<EnumStatusPayment> CheckPaymentAsync(SrvPayment srvPayment, CancellationToken cancellationToken);
    }

    public class SrvPaymentService : ISrvPaymentService
    {
        private readonly ILogger<SrvPaymentService> _logger;
        private readonly IMapper _mapper;
        private readonly IMdlPaymentRepository _paymentRepository;
        private readonly ISrvOrderService _orderService;

        public SrvPaymentService(ILogger<SrvPaymentService> logger, IMapper mapper, IMdlPaymentRepository mdlPaymentRepository, ISrvOrderService orderService)
        {
            _logger = logger;
            _mapper = mapper;
            _paymentRepository = mdlPaymentRepository;
            _orderService = orderService;
        }


        public async Task<SrvPayment> GetPaymentByOrderIdAsync(int orderId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start GetPaymentByOrderId");

            MdlPayment? mdlPayment = await _paymentRepository.FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);
            if(mdlPayment != null)
            {
                SrvPayment srvPayment = _mapper.Map<SrvPayment>(mdlPayment);

                _logger.LogInformation("End GetPaymentByOrderId");

                return srvPayment;
            }
            else
            {
                _logger.LogError("Payment not found");

                throw new Exception("Payment not found");
            }
        }

        public async Task InsertPaymentAsync(SrvPayment srvPayment, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start InsertPaymentAsync");

            MdlPayment mdlPayment = _mapper.Map<MdlPayment>(srvPayment);
            await _paymentRepository.InsertAsync(mdlPayment, cancellationToken);

            _logger.LogInformation("End InsertPaymentAsync");
        }

        public async Task UpdatePaymentAsync(SrvPayment srvPayment, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start UpdatePaymentAsync");

            MdlPayment mdlPayment = _mapper.Map<MdlPayment>(srvPayment);
            await _paymentRepository.UpdateAsync(mdlPayment, cancellationToken);

            _logger.LogInformation("End UpdatePaymentAsync");
        }

        public async Task<EnumStatusPayment> CheckPaymentAsync(SrvPayment srvPayment, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start CheckPaymentAsync");

            SrvOrder order = await _orderService.GetOrderAsync(srvPayment.OrderId, cancellationToken);

            EnumStatusPayment status = EnumStatusPayment.Completed;
            if(order.TotalAmount != srvPayment.Amount)
            {
                status = EnumStatusPayment.Failed;
            }

            if(srvPayment.PaymentDate < srvPayment.CreateAt)
            {
                status = EnumStatusPayment.Failed;
            }

            _logger.LogInformation("End CheckPaymentAsync");

            return status;
        }
    }
}
