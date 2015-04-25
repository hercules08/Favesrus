using Favesrus.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Model.Entity
{ 
    public class WishList:AbstractEntityBase
    {
        public virtual string WishListName { get; set; }
        public virtual ICollection<GiftItem> GiftItems { get; set; }
    }
}
