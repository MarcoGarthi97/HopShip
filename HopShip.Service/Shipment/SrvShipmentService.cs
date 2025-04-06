using AutoMapper;
using HopShip.Data.DTO.Repository;
using HopShip.Data.DTO.Service;
using HopShip.Repository.Shipment;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Service.Shipment
{
    public interface ISrvShipmentService
    {
        public Task<SrvShipment> GetShipmentAsync(int orderId, CancellationToken cancellationToken);
        public Task InsertShipmentAsync(SrvShipment srvShipment, CancellationToken cancellationToken);
        public Task UpdateShipmentAsync(SrvShipment srvShipment, CancellationToken cancellationToken);
    }

    public class SrvShipmentService : ISrvShipmentService
    {
        private readonly ILogger<SrvShipmentService> _logger;
        private readonly IMapper _mapper;
        private readonly IMdlShipmentRepository _shipmentRepository;

        public SrvShipmentService(ILogger<SrvShipmentService> logger, IMapper mapper, IMdlShipmentRepository mdlShipmentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _shipmentRepository = mdlShipmentRepository;
        }

        public async Task<SrvShipment> GetShipmentAsync(int orderId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start GetShipmentAsync");

            MdlShipment mdlShipment = await _shipmentRepository.FirstAsync(x => x.OrderId == orderId && x.ShipmentStatus == Data.Enum.EnumStatusShipment.Pending, cancellationToken);
            SrvShipment srvShipment = _mapper.Map<SrvShipment>(mdlShipment);

            _logger.LogInformation("End GetShipmentAsync");

            return srvShipment;
        }

        public async Task InsertShipmentAsync(SrvShipment srvShipment, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start InsertShipmentAsync");

            MdlShipment mdlShipment = _mapper.Map<MdlShipment>(srvShipment);
            await _shipmentRepository.InsertAsync(mdlShipment, cancellationToken);

            _logger.LogInformation("End InsertShipmentAsync");
        }

        public async Task UpdateShipmentAsync(SrvShipment srvShipment, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start UpdateShipmentAsync");

            MdlShipment mdlShipment = _mapper.Map<MdlShipment>(srvShipment);
            await _shipmentRepository.UpdateAsync(mdlShipment, cancellationToken);

            _logger.LogInformation("End UpdateShipmentAsync");
        }
    }
}
