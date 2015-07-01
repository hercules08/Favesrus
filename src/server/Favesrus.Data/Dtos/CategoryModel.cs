
namespace Favesrus.Data.Dtos
{
    public class CategoryModel:BaseCategoryModel
    {
    }

    public class CategoryModel2:EntityBaseModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string BackgroundColor { get; set; }
    }
}