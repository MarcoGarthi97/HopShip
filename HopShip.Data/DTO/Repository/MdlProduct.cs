﻿using HopShip.Data.Context;
using HopShip.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HopShip.Data.DTO.Repository
{
    [Table("products")]
    public class MdlProduct : BaseEntity
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("category")]
        public EnumCategoryProduct Category { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }
    }
}