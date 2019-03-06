using System.Collections.Generic;
using System.Threading.Tasks;
using BasketService.DomainModels;
using BasketService.DTOs;

namespace BasketService.Services
{
    public interface IVoucherService
    {
        Task<decimal> DeductGiftVouchers(IEnumerable<string> voucherCodes, decimal amount);
        string CheckVoucherIsValidAsync(BasketDTO basket, OfferVoucherDomainModel voucher);
        Task<OfferVoucherDomainModel> GetOfferVoucherAsync(string voucherCode);
    }
}
