using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketService.EntityModels
{
    public class ProductEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid ProductTypeId { get; set; }

        [ForeignKey(nameof(ProductTypeId))]
        public virtual ProductTypeEntity ProductType { get; set; }
    }
}
