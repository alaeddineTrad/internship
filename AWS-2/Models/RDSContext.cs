using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;

namespace AWS_2.Models
{
    public class RDSContext : DbContext
    {

        public RDSContext() : base("ebdb")
        {
            Database.SetInitializer<RDSContext>(null);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Item> Objects { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Reciever> Recievers { get; set; }
        public DbSet<Travler> Travlers { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public static RDSContext Create()
        {
            return new RDSContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // IMPORTANT: we are mapping the entity User to the same table as the entity ApplicationUser
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
        public DbQuery<T> Query<T>() where T : class
        {
            return Set<T>().AsNoTracking();
        }

        public System.Data.Entity.DbSet<AWS_2.Models.User> Users { get; set; }
    }
}
