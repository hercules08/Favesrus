using System;
using System.Collections.Generic;

namespace Favit.Server.Models
{
    public partial class Category
    {
        public Category()
        {
            this.Items = new List<Item>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
