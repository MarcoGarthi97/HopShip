using HopShip.Data.Enum;

namespace HopShip.Data.DTO.Request
{
    public class InsertProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Stock { get; set; }
        public EnumCategoryProduct Category { get; set; }
    }
}
