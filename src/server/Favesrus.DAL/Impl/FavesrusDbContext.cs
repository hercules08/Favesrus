using Favesrus.Common;
using Favesrus.DAL.Abstract;
using Favesrus.Model.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.DAL.Impl
{
    public class FavesrusDbContext : IdentityDbContext<FavesrusUser>, IFavesrusDbContext
    {
        static FavesrusDbContext()
        {
            Database.SetInitializer(new FavesrusDbInit());
        }
        
        public FavesrusDbContext(): base(Constants.DB_NAME) { }
        public FavesrusDbContext(string dbNameOrContext) : base(dbNameOrContext) { }

        public static FavesrusDbContext Create()
        {
            return new FavesrusDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin>().ToTable("AspNetUserLogins")
                .Property(c => c.ProviderKey).HasMaxLength(Int32.MaxValue);
        }

    }
}
