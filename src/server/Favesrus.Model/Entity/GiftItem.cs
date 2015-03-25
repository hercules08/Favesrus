using Favesrus.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Model.Entity
{
    public class GiftItem : AbstractEntityBase
    {
        public GiftItem()
        {
        }

        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        //public string Description { get; set; }
        public Nullable<decimal> ItemPrice { get; set; }
        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Retailer> Retailer { get; set; }
    }
}
