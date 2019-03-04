using System;
namespace BasketService.DTOs
{
    public class CreatBasketItemDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
