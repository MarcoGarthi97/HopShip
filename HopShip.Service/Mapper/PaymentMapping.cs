using AutoMapper;
using HopShip.Data.DTO.Repository;
using HopShip.Data.DTO.Service;

namespace HopShip.Service.Mapper
{
    public class PaymentMapping : Profile
    {
        public PaymentMapping()
        {
            CreateMap<SrvPayment, MdlPayment>().ReverseMap();
        }
    }
}
