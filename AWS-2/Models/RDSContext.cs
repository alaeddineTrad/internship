using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;

namespace AWS_2.Models
{
    public class RDSContext : DbContext
    {

        public RDSContext() : base("ebdb")
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Objects { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public static RDSContext Create()
        {
            return new RDSContext();
        }
        


    }
}
