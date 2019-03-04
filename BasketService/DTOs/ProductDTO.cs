using System;
namespace BasketService.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid ProductTypeId { get; set; }
    }
}
