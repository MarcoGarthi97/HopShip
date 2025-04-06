using HopShip.Data.DTO.Service;
using HopShip.Data.Enum;
using Microsoft.Extensions.Logging;

namespace HopShip.Service.Shipment
{
    public interface ISrvShipmentBuilderService
    {
        public SrvShipment BuildShipment(SrvShipment shipment);
    }

    public class SrvShipmentBuilderService : ISrvShipmentBuilderService
    {
        private readonly ILogger<SrvShipmentBuilderService> _logger;

        public SrvShipmentBuilderService(ILogger<SrvShipmentBuilderService> logger)
        {
            _logger = logger;
        }

        public SrvShipment BuildShipment(SrvShipment shipment)
        {
            _logger.LogInformation("Start BuildShipment");

            shipment.ShipmentStatus = BuildStatusShipment();
            if(shipment.ShipmentStatus != EnumStatusShipment.Error)
            {
                shipment.ShipmentDate = BuildShipmentDate();
                shipment.TrackingNumber = BuildTranckingNumber();
            }

            _logger.LogInformation("End BuildShipment");

            return shipment;
        }

        private EnumStatusShipment BuildStatusShipment()
        {
            _logger.LogInformation("Start BuildStatusShipment");

            int random = new Random().Next(1, 9);

            _logger.LogInformation("End BuildStatusShipment");

            return (EnumStatusShipment)random;
        }

        private DateTime BuildShipmentDate()
        {
            _logger.LogInformation("Start BuildShipmentDate");

            int random = new Random().Next(0, 10);

            _logger.LogInformation("End BuildShipmentDate");

            return DateTime.Now.AddDays(random);
        }

        private string BuildTranckingNumber()
        {
            _logger.LogInformation("Start BuildTranckingNumber");

            string tranckingNumber = string.Empty;
            while(tranckingNumber.Length < 11)
            {
                int random = new Random().Next(48, 127);
                if (char.IsLetterOrDigit((char)random))
                {
                    tranckingNumber += (char)random;
                }
            }

            _logger.LogInformation("End BuildTranckingNumber");

            return tranckingNumber;
        }
    }
}
