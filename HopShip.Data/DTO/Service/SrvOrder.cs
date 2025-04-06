using HopShip.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Data.DTO.Service
{
    public class SrvOrder
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public EnumStatusOrder Status { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
