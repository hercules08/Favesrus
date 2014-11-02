using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Project_G.Server.Models.Mapping;

namespace Project_G.Server.Models
{
    public partial class ProjectG_DBContext : DbContext
    {
        static ProjectG_DBContext()
        {
            Database.SetInitializer<ProjectG_DBContext>(null);
        }

        public ProjectG_DBContext()
            : base("Name=ProjectG_DBContext")
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Occasion> Occasions { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<User> Users { get; set; }

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
