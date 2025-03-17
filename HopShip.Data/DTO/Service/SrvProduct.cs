using HopShip.Data.Enum;

namespace HopShip.Data.DTO.Service
{
    public class SrvProduct
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Stock { get; set; }
        public EnumCategoryProduct Category { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
