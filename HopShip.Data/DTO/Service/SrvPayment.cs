using HopShip.Data.Enum;

namespace HopShip.Data.DTO.Service
{
    public class SrvPayment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public EnumStatusPayment PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public EnumMethodPayment PaymentMethod { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
