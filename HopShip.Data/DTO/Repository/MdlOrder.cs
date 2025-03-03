using HopShip.Data.Context;
using HopShip.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopShip.Data.DTO.Repository
{
    [Table("orders")]
    public class MdlOrder : BaseEntity
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }
        [Column("userid")]
        public string UserId { get; set; }
        [Column("totalamount")]
        public decimal TotalAmount { get; set; }
        [Column("status")]
        public EnumStatusOrder Status { get; set; }
        [Column("createdat")]
        public DateTime CreateDate { get; set; }
    }
}