using AutoMapper;
using HopShip.Data.DTO.RabbitMQ;
using HopShip.Data.DTO.Repository;
using HopShip.Data.DTO.Request;
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
            CreateMap<InsertOrderRequest, SrvOrder>();
            CreateMap<SrvOrder, QueueMessageRabbitMQ>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(from => from.Id));
        }
    }
}
