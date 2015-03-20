using Favesrus.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Model.Entity
{
    public class Retailer:AbstractEntityBase
    {
        public virtual string RetailerName { get; set; }
        public virtual string RetailerLogo { get; set; }
        public virtual string RetailerLogoDataString { get; set; }
        public virtual string ModoMerchantId { get; set; }
        public virtual ICollection<GiftItem> Items { get; set; }
    }
}
