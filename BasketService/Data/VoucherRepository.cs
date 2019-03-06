using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BasketService.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace BasketService.Data
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly BasketDbContext _dbContext;
        private readonly IMapper _mapper;

        public VoucherRepository(BasketDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<decimal> GetGiftVouchersDeductionAsync(IEnumerable<string> codes) =>
            await _dbContext.GiftVouchers
                    .Where(gv => codes.Contains(gv.Code))
                    .Select(g => g.Amount)
                    .SumAsync();

        public async Task<OfferVoucherDomainModel> GetOfferVoucherAsync(string code) =>
            _mapper.Map<OfferVoucherDomainModel>(
                await _dbContext.OfferVouchers.SingleOrDefaultAsync(ov => ov.Code.Equals(code)));
    }
}
