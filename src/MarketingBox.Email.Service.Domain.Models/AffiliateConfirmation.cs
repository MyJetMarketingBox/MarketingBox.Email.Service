using System;

namespace MarketingBox.Email.Service.Domain.Models
{
    public class AffiliateConfirmation
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public long AffiliateId { get; set; }
        public string Token { get; set; }
    }
}