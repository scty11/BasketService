using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasketService.DomainModels;

namespace BasketService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDomainModel>> GetProductsAsync(IEnumerable<Guid> ids);
    }
}
