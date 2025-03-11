using HopShip.Library.Context;
using HopShip.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopShip.Data.DTO.Repository
{
    [Table("shipments")]
    public class MdlShipment : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("orderid")]
        public string OrderId { get; set; }

        [Column("shipmentstatus")]
        public EnumStatusShipment ShipmentStatus { get; set; }

        [Column("shipmentdate")]
        public DateTime ShipmentDate { get; set; }

        [Column("trackingnumber")]
        public string TrackingNumber { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }
    }
}