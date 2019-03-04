using AutoMapper;
using BasketService.DomainModels;
using BasketService.DTOs;
using BasketService.EntityModels;

namespace BasketService.Mappers
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductEntity, ProductDomainModel>();
            CreateMap<ProductDomainModel, ProductDTO>();
        }
    }
}
