using Favit.Model.Base;
using System;
using System.Collections.Generic;

namespace Favit.Model.Entities
{
    public partial class Retailer : EntityBase
    {
        public virtual string RetailerName { get; set; }
        public virtual string RetailerLogo { get; set; }
        public virtual string RetailerLogoDataString { get; set; }
        public virtual string ModoMerchantId { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
