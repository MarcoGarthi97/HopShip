using HopShip.Data.Enum;

namespace HopShip.Data.DTO.Service
{
    public class SrvShipment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public EnumStatusShipment ShipmentStatus { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
