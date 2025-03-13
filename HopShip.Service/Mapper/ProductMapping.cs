using AutoMapper;
using HopShip.Data.DTO.Repository;
using HopShip.Data.DTO.Request;
using HopShip.Data.DTO.Service;

namespace HopShip.Service.Mapper
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<InsertProductRequest, SrvProduct>();
            CreateMap<SrvProduct, MdlProduct>().ReverseMap();
        }
    }
}
