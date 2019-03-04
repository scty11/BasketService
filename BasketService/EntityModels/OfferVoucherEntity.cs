using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketService.EntityModels
{
    public class OfferVoucherEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal ThresHold { get; set; }
        public decimal Amount { get; set; }
        public Guid? ProductTypeId { get; set; }
        public string Code { get; set; }

        [ForeignKey(nameof(ProductTypeId))]
        public virtual ProductTypeEntity ProductType { get; set; }
    }
}
