using System;
using System.Collections.Generic;

namespace BasketService.DTOs
{
    public class BasketDTO
    {
        public IEnumerable<BasketItemDTO> BasketItems { get; set; }
        public decimal BasketTotal { get; set; }
        public decimal BasketDiscountTotal { get; set; }
    }
}
