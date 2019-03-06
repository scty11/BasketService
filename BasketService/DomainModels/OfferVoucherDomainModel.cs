using System;

namespace BasketService.DomainModels
{
    public class OfferVoucherDomainModel
    {
        public decimal ThresHold { get; set; }
        public decimal Amount { get; set; }
        public Guid? ProductTypeId { get; set; }
        public string Code { get; set; }
    }
}
