using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketService.EntityModels
{
    public class ProductTypeEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}