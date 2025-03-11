using AutoMapper;
using HopShip.Data.DTO.Repository;
using HopShip.Data.DTO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Service.Mapper
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<MdlOrder, SrvOrder>().ReverseMap();
        }
    }
}
