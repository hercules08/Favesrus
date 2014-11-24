using Favit.Model.Base;
using System;
using System.Collections.Generic;

namespace Favit.Model.Entities
{
    public partial class Item : EntityBase
    {
        public Item()
        {
        }

        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public Nullable<decimal> ItemPrice { get; set; }
        public Nullable<int> RetailerId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Retailer Retailer { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
