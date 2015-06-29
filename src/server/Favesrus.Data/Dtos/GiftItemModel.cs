using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Favesrus.Data.Dtos
{
    public class GiftItemModel : EntityBaseModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        [DisplayName("Categories/Recommendations")]
        public ICollection<BaseCategoryModel> Category {get;set;}
        [DisplayName("Retailers")]
        public ICollection<RetailerModel> Retailer { get; set; }
        [DisplayName("WishLists")]
        public ICollection<WishListModel> WishList { get; set; }
    }
    
    public class GiftItemModel2 : EntityBaseModel
    {
        public GiftItemModel2()
        {
            Category = "Unknown";
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        [DisplayName("Categories/Recommendations")]
        public string Category{ get; set; }
    }
}