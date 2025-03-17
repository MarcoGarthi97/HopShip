using AutoMapper;
using HopShip.Data.DTO.Repository;
using HopShip.Data.DTO.Request;
using HopShip.Data.DTO.Service;

namespace HopShip.Service.Mapper
{
    public class OrderProductMapping : Profile
    {
        public OrderProductMapping()
        {
            CreateMap<InsertOrderProductRequest, SrvOrderProduct>();
            CreateMap<SrvOrderProduct, MdlOrderProduct>().ReverseMap();
        }
    }
}
