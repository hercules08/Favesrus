using Favesrus.Domain.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Favesrus.DAL.Mapping
{
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public CategoryMapping()
        {
            ToTable("Categories");
        }
    }
}
