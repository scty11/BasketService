using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketService.EntityModels
{
    public class ProductEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ProductTypeId { get; set; }

        [ForeignKey(nameof(ProductTypeId))]
        public virtual ProductTypeEntity ProductType { get; set; }
    }
}
