using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketService.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace BasketService.Data
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly BasketDbContext _dbContext;

        public VoucherRepository(BasketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<decimal> GetGiftVouchersDeductionAsync(IEnumerable<string> codes) =>
            await _dbContext.GiftVouchers
                    .Where(gv => codes.Contains(gv.Code))
                    .Select(g => g.Amount)
                    .SumAsync();

        public async Task<OfferVoucherEntity> GetOfferVoucherAsync(string code) =>
            await _dbContext.OfferVouchers.SingleOrDefaultAsync(ov => ov.Code.Equals(code));
    }
}
