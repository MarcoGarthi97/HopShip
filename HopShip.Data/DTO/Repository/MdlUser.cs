using HopShip.Library.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopShip.Data.DTO.Repository
{
    [Table("users")]
    public class MdlUser : BaseEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }
    }
}