using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ProjectG.Server.Models.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CategoryName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Categories");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
        }
    }
}
