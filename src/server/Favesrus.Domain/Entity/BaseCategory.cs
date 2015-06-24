using Favesrus.Domain.Base;
using System.Collections.Generic;

namespace Favesrus.Domain.Entity
{
    public abstract class BaseCategory:EntityBase
    {
        public virtual string CategoryName { get; set; }
        public virtual ICollection<GiftItem> GiftItems { get; set; }
        public virtual string CategoryImage { get; set; }
    }
}
