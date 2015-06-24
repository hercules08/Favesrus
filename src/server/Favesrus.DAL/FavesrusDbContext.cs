using Favesrus.Core;
using Favesrus.DAL.Mapping;
using Favesrus.Domain.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Favesrus.DAL
{
    public class FavesrusDbContext : IdentityDbContext<FavesrusUser>
    {
        static FavesrusDbContext()
        {
            if (DebuggingService.RunningInDebugMode())
            {
                Database.SetInitializer(new FavesrusDbInit());
            }
            else
            {
                Database.SetInitializer(new NullFavesrusDbInit());
            }
        }

        public static FavesrusDbContext Create()
        {
            return new FavesrusDbContext();
        }
        
        public FavesrusDbContext(): base(FavesrusConstants.DB_NAME) { }
        public FavesrusDbContext(string dbNameOrContext) : base(dbNameOrContext) { }

        public DbSet<GiftItem> GiftItems { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CategoryMapping());
            modelBuilder.Configurations.Add(new RecommendationMapping());
            modelBuilder.Configurations.Add(new IdentityUserLoginMapping());

        }
    }
}
