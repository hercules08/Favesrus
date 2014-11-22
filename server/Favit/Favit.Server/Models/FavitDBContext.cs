using Favit.Server.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Favit.Server.Models
{
    public partial class FavitDBContext : DbContext, IDbContext
    {
        static FavitDBContext()
        {
            Database.SetInitializer<FavitDBContext>(null);
            //Database.SetInitializer<ProjectGDBContext>(new DropCreateDatabaseIfModelChangesWithSeedData());
        }

        public FavitDBContext()
            : base("Name=ProjectGDBContext")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Occasion> Occasions { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<VoteModel> VoteModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public new IDbSet<T> Set<T>() where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}

