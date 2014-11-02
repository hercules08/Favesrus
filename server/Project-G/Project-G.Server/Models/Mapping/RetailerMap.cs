using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Project_G.Server.Models.Mapping
{
    public class RetailerMap : EntityTypeConfiguration<Retailer>
    {
        public RetailerMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.RetailerName)
                .HasMaxLength(50);

            this.Property(t => t.RetailerLogo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Retailers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RetailerName).HasColumnName("RetailerName");
            this.Property(t => t.RetailerLogo).HasColumnName("RetailerLogo");
            this.Property(t => t.RetailerLogoDataString).HasColumnName("RetailerLogoDataString");
        }
    }
}
