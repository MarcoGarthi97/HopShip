using HopShip.Library.Context;
using HopShip.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopShip.Data.DTO.Repository
{
    [Table("payments")]
    public class MdlPayment : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("orderid")]
        public string OrderId { get; set; }

        [Column("paymentstatus")]
        public EnumStatusPayment PaymentStatus { get; set; }

        [Column("paymentdate")]
        public DateTime PaymentDate { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("paymentmethod")]
        public EnumMethodPayment PaymentMethod { get; set; }

        [Column("createdat")]
        public int CreatedAt { get; set; }
    }
}