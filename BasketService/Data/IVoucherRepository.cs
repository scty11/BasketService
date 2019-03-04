using System.Collections.Generic;
using System.Threading.Tasks;
using BasketService.EntityModels;

namespace BasketService.Data
{
    public interface IVoucherRepository
    {
        Task<decimal> GetGiftVouchersDeductionAsync(IEnumerable<string> codes);
        Task<OfferVoucherEntity> GetOfferVoucherAsync(string code);
    }
}
