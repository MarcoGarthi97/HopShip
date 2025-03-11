namespace HopShip.Data.DTO.Request
{
    public class InsertOrderRequest
    {
        public int UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public IEnumerable<InsertOrderProductRequest> Products { get; set; }
    }
}
