using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ProjectG.Server.Models
{
    public partial class ProjectGDBContext : DbContext
    {
        static ProjectGDBContext()
        {
            //Database.SetInitializer<ProjectGDBContext>(null);
            Database.SetInitializer<ProjectGDBContext>(new DropCreateDatabaseIfModelChangesWithSeedData());
        }

        public ProjectGDBContext()
            : base("Name=ProjectGDBContext")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Occasion> Occasions { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}

