using Favit.DAL.Mapping;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Favit.Model.Entities;
using Ninject;

namespace Favit.DAL
{
    public partial class FavitDBContext : DbContext
    {
        static FavitDBContext()
        {
            Database.SetInitializer<FavitDBContext>(null);
            //Database.SetInitializer<FavitDBContext>(new FavitInitializer());
        }

        public FavitDBContext()
            : base("Name=FavitDBContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Occasion> Occasions { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VoteModel> VoteModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new ItemMap());
            modelBuilder.Configurations.Add(new OccasionMap());
            modelBuilder.Configurations.Add(new RetailerMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}