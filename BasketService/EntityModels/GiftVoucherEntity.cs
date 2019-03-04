using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasketService.EntityModels
{
    public class GiftVoucherEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
    }
}
