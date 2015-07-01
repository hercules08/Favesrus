using Favesrus.Domain.Base;
using System.Collections.Generic;

namespace Favesrus.Domain.Entity
{
    public abstract class BaseCategory:EntityBase
    {
        public string CategoryName { get; set; }
        public virtual ICollection<GiftItem> GiftItems { get; set; }
        public string CategoryImage { get; set; }
        public string BackgroundColor { get; set; }
    }
}
