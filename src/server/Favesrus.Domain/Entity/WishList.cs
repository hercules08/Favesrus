using Favesrus.Domain.Base;
using System.Collections.Generic;

namespace Favesrus.Domain.Entity
{ 
    public class WishList:EntityBase
    {
        public string WishListName { get; set; }
        public virtual ICollection<GiftItem> GiftItems { get; set; }

        public void AddGiftItem(GiftItem foundItem)
        {
            GiftItems.Add(new GiftItem { Id = foundItem.Id });
        }
    }
}
