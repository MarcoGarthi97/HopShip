using HopShip.Library.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopShip.Data.DTO.Repository
{
    [Table("orderproducts")]
    public class MdlOrderProduct : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("orderid")]
        public int OrderId { get; set; }
        [Column("productid")]
        public int ProductId { get; set; }
        [Column("stock")]
        public int Stock { get; set; }
        [Column("unitprice")]
        public decimal UnitPrice { get; set; }
        [Column("discount")]
        public decimal Discount { get; set; }
        [Column("totalprice")]
        public decimal TotalPrice { get; set; }
        [Column("createddate")]
        public DateTime CreateDate { get; set; }
    }
}