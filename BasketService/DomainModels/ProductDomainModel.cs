using System;
namespace BasketService.DomainModels
{
    public class ProductDomainModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid ProductTypeId { get; set; }
    }
}
