using Favesrus.Domain.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Favesrus.DAL.Mapping
{
    public class RecommendationMapping : EntityTypeConfiguration<Recommendation>
    {
        public RecommendationMapping()
        {
            ToTable("Recommendations");
        }
    }
}
