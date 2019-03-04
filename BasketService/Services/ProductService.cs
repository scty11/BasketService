using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasketService.Data;
using BasketService.DomainModels;

namespace BasketService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<IEnumerable<ProductDomainModel>> GetProductsAsync(IEnumerable<Guid> ids) =>
            _productRepository.GetProductsAsync(ids);
    }
}
