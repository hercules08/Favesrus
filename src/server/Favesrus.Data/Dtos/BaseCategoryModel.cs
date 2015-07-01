using System.Collections.Generic;

namespace Favesrus.Data.Dtos
{
    public class BaseCategoryModel:EntityBaseModel
    {
        public string Name { get; set; }
        public ICollection<GiftItemModel> GiftItems { get; set; }
        public string Image { get; set; }
        public string BackgroundColor { get; set; }
    }
}