using Favesrus.Domain.Base;
using System.Collections.Generic;

namespace Favesrus.Domain.Entity
{
    public class Retailer:EntityBase
    {
        public string RetailerName { get; set; }
        public string RetailerLogo { get; set; }
        public string RetailerLogoDataString { get; set; }
        public string ModoMerchantId { get; set; }
        public virtual ICollection<GiftItem> Items { get; set; }
    }
}
