using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Project_G.Server.Models.Mapping
{
    public class OccasionMap : EntityTypeConfiguration<Occasion>
    {
        public OccasionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Occasion1)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Occasions");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Occasion1).HasColumnName("Occasion");
            this.Property(t => t.OccasionDate).HasColumnName("OccasionDate");
        }
    }
}
