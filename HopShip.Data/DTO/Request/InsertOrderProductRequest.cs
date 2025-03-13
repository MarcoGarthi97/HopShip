namespace HopShip.Data.DTO.Request
{
    public class InsertOrderProductRequest
    {
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
