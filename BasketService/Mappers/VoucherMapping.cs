using AutoMapper;
using BasketService.DomainModels;
using BasketService.EntityModels;

namespace BasketService.Mappers
{
    public class VoucherMapping : Profile
    {
        public VoucherMapping()
        {
            CreateMap<OfferVoucherEntity, OfferVoucherDomainModel>();
        }
    }
}
