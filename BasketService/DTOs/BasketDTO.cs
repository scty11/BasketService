using System.Collections.Generic;
using System.Linq;

namespace BasketService.DTOs
{
    public class BasketDTO
    {
        public ICollection<BasketItemDTO> BasketItems { get; set; } = new List<BasketItemDTO>();
        public decimal BasketTotal { get; set; }
        public decimal BasketDiscountTotal { get; set; }
        public string Message { get; set; }
    }
}
