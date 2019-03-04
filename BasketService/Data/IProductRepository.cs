using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasketService.DomainModels;

namespace BasketService.Data
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDomainModel>> GetProductsAsync(IEnumerable<Guid> ids);
    }
}
