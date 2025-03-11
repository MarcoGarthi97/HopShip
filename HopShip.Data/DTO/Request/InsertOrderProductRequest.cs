namespace HopShip.Data.DTO.Request
{
    public class InsertOrderProductRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int TotalPrice { get; set; }
    }
}
