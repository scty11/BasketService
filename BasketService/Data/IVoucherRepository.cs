using System.Collections.Generic;
using System.Threading.Tasks;
using BasketService.DomainModels;

namespace BasketService.Data
{
    public interface IVoucherRepository
    {
        Task<decimal> GetGiftVouchersDeductionAsync(IEnumerable<string> codes);
        Task<OfferVoucherDomainModel> GetOfferVoucherAsync(string code);
    }
}
