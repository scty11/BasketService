using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasketService.DomainModels;
using BasketService.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace BasketService.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly BasketDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(BasketDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDomainModel>> GetProductsAsync(IEnumerable<Guid> ids) =>
            (await _dbContext.Products
                   .Where(p => ids.Contains(p.Id))
                   .ToListAsync())
                   .Select(_mapper.Map<ProductDomainModel>);
    }
}
