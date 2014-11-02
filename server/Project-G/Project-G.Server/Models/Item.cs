using System;
using System.Collections.Generic;

namespace Project_G.Server.Models
{
    public partial class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public Nullable<decimal> ItemPrice { get; set; }
        public Nullable<int> RetailerId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Retailer Retailer { get; set; }
    }
}
