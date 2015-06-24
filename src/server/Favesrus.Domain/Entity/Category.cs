using System.Collections.Generic;

namespace Favesrus.Domain.Entity
{
    public class Category : BaseCategory
    {
        //internal Category()
        //{
        //    this.GiftItems = new List<GiftItem>();
        //}

        public Category()
        {

        }

        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }

        public Category(string categoryName, ICollection<GiftItem> items) :
            this(categoryName)
        {
            GiftItems = items;
        }

        public void AddGiftItemToCategory(GiftItem item)
        {
            GiftItems.Add(item);
        }

        //public override bool IsValid
        //{
        //    get
        //    {
        //        return base.Validate<Category>();
        //    }
        //}
    }
}
