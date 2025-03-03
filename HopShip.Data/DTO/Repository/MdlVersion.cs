using HopShip.Data.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Data.DTO.Repository
{
    [Table("version")]
    public class MdlVersion : BaseEntity
    {
        [Key]
        [Column("version")]
        public string Version { get; set; }
        [Column("rud")]
        public DateTime RUD { get; set;}
    }
}