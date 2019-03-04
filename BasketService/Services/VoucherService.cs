using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketService.Data;
using BasketService.DTOs;

namespace BasketService.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<decimal> DeductGiftVouchers(IEnumerable<string> voucherCodes, decimal basketAmount)
        {
            var voucherAmount = await _voucherRepository.GetGiftVouchersDeductionAsync(voucherCodes);
            var newAmount = basketAmount - voucherAmount;

            return newAmount < 0 ? 0.00M : newAmount;
        }

        public async Task<string> CheckVoucherIsValidAsync(BasketDTO basket, string voucherCode)
        {
            var voucher = await _voucherRepository.GetOfferVoucherAsync(voucherCode);
            var giftVouchers = basket.BasketItems.Where(bi => bi.Product.Description == "Voucher");
            var deductGiftAmount = 0.00M;
            if (giftVouchers.Any())
            {
                foreach (var item in giftVouchers)
                {
                    deductGiftAmount = item.Quantity * item.Product.Price;
                }
            }

            if (voucher.ProductTypeId.HasValue)
            {
                if (!basket.BasketItems.Any(bi => bi.Product.ProductTypeId == voucher.ProductTypeId.Value))
                {
                    return $"There are no products in your basket applicable to voucher {voucherCode}.";
                }

                if (basket.BasketTotal - deductGiftAmount >= voucher.ThresHold)
                {
                    return string.Empty;
                }
                return $"You have not reached the spend threshold for voucher {voucherCode}. " +
                	        $"Spend another £{voucher.ThresHold - (basket.BasketTotal - deductGiftAmount) + 0.01M}" +
                            $"receive £{String.Format("{0:0.00}", voucher.Amount)} discount from your basket total.";
            }
            if (basket.BasketTotal - deductGiftAmount >= voucher.ThresHold)
            {
                return string.Empty;
            }
            return $"You have not reached the spend threshold for voucher {voucherCode}. " +
                       $"Spend another £{voucher.ThresHold - (basket.BasketTotal - deductGiftAmount) + 0.01M} " +
                       $"receive £{String.Format("{0:0.00}",voucher.Amount)} discount from your basket total.";
        }

        public async Task<decimal> DeductOfferVoucher(string voucherCode, decimal basketAmount)
        {
            var voucher = await _voucherRepository.GetOfferVoucherAsync(voucherCode);
            return basketAmount - voucher.Amount;
        }
    }
}
