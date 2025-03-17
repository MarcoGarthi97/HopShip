namespace HopShip.Data.DTO.Service
{
    public class SrvOrderProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
