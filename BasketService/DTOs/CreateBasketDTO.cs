using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketService.DTOs
{
    public class CreateBasketDTO
    {
        public IEnumerable<CreatBasketItemDTO> Products { get; set; } = Enumerable.Empty<CreatBasketItemDTO>();
        public IEnumerable<string> GiftVoucherCodes { get; set; } = Enumerable.Empty<string>();
        public string OfferVoucherCode { get; set; }
    }
}
