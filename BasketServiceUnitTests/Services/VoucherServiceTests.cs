using System.Collections.Generic;
using System.Threading.Tasks;
using BasketService.Data;
using BasketService.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace BasketServiceUnitTests.Services
{
    public class VoucherServiceTests
    {
        private readonly Mock<IVoucherRepository> _voucherRepository;
        private readonly VoucherService _voucherService;
        private const decimal giftVoucherAmount = 10.00M;

        public VoucherServiceTests()
        {
            _voucherRepository = new Mock<IVoucherRepository>();
            _voucherService = new VoucherService(_voucherRepository.Object);

            _voucherRepository.Setup(
                v => v.GetGiftVouchersDeductionAsync(It.IsAny<IEnumerable<string>>()))
                    .ReturnsAsync(giftVoucherAmount);
        }

        [Fact(DisplayName = @"Given gift vouchers When the basket amount is greater than the 
            voucher amounts Then deduct the correct amount")]
        public async Task DeductGiftVouchers_TotalIsGreaterThanVouchers_DeductAmount()
        {
            var basketAmount = 100.00M;
            var result = await _voucherService.DeductGiftVouchers(new List<string>(), basketAmount);

            result.Should().Be(basketAmount - giftVoucherAmount);
        }

        [Fact(DisplayName = @"Given gift vouchers When the basket amount is greater than the 
            voucher amounts Then deduct the correct amount")]
        public async Task DeductGiftVouchers_TotalIsLessThanVouchers_AmountIsZero()
        {
            var basketAmount = 5.00M;

            var result = await _voucherService.DeductGiftVouchers(new List<string>(), basketAmount);

            result.Should().Be(0.00M);
        }
    }
}
