using AutoMapper;
using HopShip.Data.DTO.Repository;
using HopShip.Data.DTO.Service;

namespace HopShip.Service.Mapper
{
    public class ShipmentMapping : Profile
    {
        public ShipmentMapping()
        {
            CreateMap<SrvShipment, MdlShipment>().ReverseMap();
        }
    }
}
