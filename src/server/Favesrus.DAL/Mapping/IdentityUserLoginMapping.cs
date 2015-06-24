using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration;

namespace Favesrus.DAL.Mapping
{
    public class IdentityUserLoginMapping : EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginMapping()
        {

            ToTable("AspNetUserLogins")
                    .Property(c => c.ProviderKey).HasMaxLength(450);
        }
    }
}
