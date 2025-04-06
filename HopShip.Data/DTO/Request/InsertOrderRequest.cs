using HopShip.Data.Enum;

namespace HopShip.Data.DTO.Request
{
    public class InsertOrderRequest
    {
        public int UserId { get; set; }
        public EnumMethodPayment PaymentMethod { get; set; }
        public IEnumerable<InsertOrderProductRequest> Products { get; set; }
    }
}
