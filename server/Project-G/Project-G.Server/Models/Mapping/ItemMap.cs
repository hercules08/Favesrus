using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Project_G.Server.Models.Mapping
{
    public class ItemMap : EntityTypeConfiguration<Item>
    {
        public ItemMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ItemName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Items");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.ItemPrice).HasColumnName("ItemPrice");
            this.Property(t => t.RetailerId).HasColumnName("RetailerId");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");

            // Relationships
            this.HasOptional(t => t.Category)
                .WithMany(t => t.Items)
                .HasForeignKey(d => d.CategoryId);
            this.HasOptional(t => t.Retailer)
                .WithMany(t => t.Items)
                .HasForeignKey(d => d.RetailerId);

        }
    }
}
