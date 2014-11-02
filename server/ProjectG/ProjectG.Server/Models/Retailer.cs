using System;
using System.Collections.Generic;

namespace ProjectG.Server.Models
{
    public partial class Retailer
    {
        public Retailer()
        {
            this.Items = new List<Item>();
        }

        public int Id { get; set; }
        public string RetailerName { get; set; }
        public string RetailerLogo { get; set; }
        public string RetailerLogoDataString { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
