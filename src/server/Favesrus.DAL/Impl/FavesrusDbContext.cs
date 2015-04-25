using Favesrus.Common;
using Favesrus.DAL.Abstract;
using Favesrus.Model.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Favesrus.DAL.Impl
{
    public class FavesrusDbContext : IdentityDbContext<FavesrusUser>, IFavesrusDbContext
    {
        static FavesrusDbContext()
        {
            if (!DebuggingService.RunningInDebugMode())
            {
                //Database.SetInitializer(new NullFavesrusDbInit());
                Database.SetInitializer(new FavesrusDbInit());
            }
            else
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
                .Property(c => c.ProviderKey).HasMaxLength(450);
        }

        //public DbSet<FavesrusUser> FavesrusUsers { get; set; }
        //public DbSet<FavesrusRole> FavesrusRoles { get; set; }
        public DbSet<GiftItem> GiftItems { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Retailer> Retailers { get; set; }

    }
}
