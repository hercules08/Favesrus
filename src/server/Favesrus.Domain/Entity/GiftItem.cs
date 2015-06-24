using Favesrus.Domain.Base;
using System;
using System.Collections.Generic;

namespace Favesrus.Domain.Entity
{
    public class GiftItem : EntityBase
    {
        public GiftItem()
        {
        }

        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> ItemPrice { get; set; }
        public virtual ICollection<BaseCategory> Category { get; set; }
        public virtual ICollection<Retailer> Retailer { get; set; }
        public virtual ICollection<WishList> WishList { get; set; }
    }
}
