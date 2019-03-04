using System.Collections.Generic;
using System.Threading.Tasks;
using BasketService.DTOs;

namespace BasketService.Services
{
    public interface IVoucherService
    {
        Task<decimal> DeductGiftVouchers(IEnumerable<string> voucherCodes, decimal amount);
        Task<string> CheckVoucherIsValidAsync(BasketDTO basket, string voucherCode);
        Task<decimal> DeductOfferVoucher(string voucherCode, decimal basketAmount);
    }
}
