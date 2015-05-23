using Favesrus.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Model.Entity
{
    public abstract class BaseCategory:AbstractEntityBase
    {
        public virtual string CategoryName { get; set; }
        public virtual ICollection<GiftItem> GiftItems { get; set; }
        public virtual string CategoryImage { get; set; }
    }
}
