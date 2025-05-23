﻿using AutoMapper;
using HopShip.Data.DTO.RabbitMQ;
using HopShip.Data.DTO.Request;
using HopShip.Data.DTO.Service;
using HopShip.Service.Order;
using HopShip.Service.OrderProduct;
using HopShip.Service.Payment;
using HopShip.Service.RabbitMQ;
using HopShip.Service.Shipment;
using Microsoft.AspNetCore.Mvc;

namespace HopShip.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;
        private readonly ISrvOrderProductService _serviceOrderProduct;
        private readonly ISrvOrderService _serviceOrder;
        private readonly ISrvPaymentService _servicePayment;
        private readonly ISrvShipmentService _serviceShipment;
        private readonly ISrvRabbitMQService _rabbitMQService;

        public OrdersController(ILogger<OrdersController> logger, IMapper mapper, ISrvOrderService srvOrderService, ISrvOrderProductService srvOrderProductService, ISrvPaymentService srvPaymentService, ISrvShipmentService srvShipmentService, ISrvRabbitMQService srvRabbitMQService)
        {
            _logger = logger;
            _mapper = mapper;
            _serviceOrderProduct = srvOrderProductService;
            _serviceOrder = srvOrderService;
            _servicePayment = srvPaymentService;
            _serviceShipment = srvShipmentService;
            _rabbitMQService = srvRabbitMQService;
        }

        [HttpPost("InsertOrder", Name = "InsertOrder")]
        public async Task<ActionResult<bool>> InsertOrdersAsync(InsertOrderRequest order, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start InsertOrdersAsync");

                IEnumerable<SrvOrderProduct> srvOrderProducts = _mapper.Map<IEnumerable<SrvOrderProduct>>(order.Products);
                srvOrderProducts = await _serviceOrderProduct.CheckOrderProductsBeforeAsync(srvOrderProducts.ToList(), cancellationToken);

                SrvOrder srvOrder = _mapper.Map<SrvOrder>(order);
                srvOrder.TotalAmount = srvOrderProducts.Sum(x => x.TotalPrice);
                srvOrder = await _serviceOrder.InsertOrdersAsync(srvOrder, cancellationToken);

                srvOrderProducts.ToList().ForEach(x => x.OrderId = srvOrder.Id);
                await _serviceOrderProduct.InsertOrderProductAsync(srvOrderProducts, cancellationToken);

                SrvPayment srvPayment = new()
                {
                    OrderId = srvOrder.Id,
                    PaymentMethod = order.PaymentMethod,
                    PaymentStatus = Data.Enum.EnumStatusPayment.InQueue,
                    PaymentDate = DateTime.Now,
                    Amount = srvOrder.TotalAmount,
                    CreatedAt = DateTime.Now
                };
                await _servicePayment.InsertPaymentAsync(srvPayment, cancellationToken);

                SrvShipment srvShipment = new()
                {
                    OrderId = srvOrder.Id,
                    CreatedAt = DateTime.Now
                };
                await _serviceShipment.InsertShipmentAsync(srvShipment, cancellationToken);

                var messageRabbit = _mapper.Map<QueueMessageRabbitMQ>(srvOrder);

                await _rabbitMQService.EnqueueMessageAsync(Data.Enum.EnumQueueRabbit.OrderService, messageRabbit);

                _logger.LogInformation("End InsertOrdersAsync");

                return Ok(true);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                throw ex;
            }
        }
    }
}
