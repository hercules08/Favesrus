using System.Collections.Generic;

namespace Favesrus.Data.Dtos
{
    public class WishListModel:EntityBaseModel
    {
        public string WishListName { get; set; }
        public ICollection<GiftItemModel> GiftItems { get; set; }
    }

    public class WishListModel2:EntityBaseModel
    {
        public string WishListName { get; set; }
        public ICollection<GiftItemModel2> GiftItems { get; set; }
    }
}