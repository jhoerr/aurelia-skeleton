using System.Data.Common;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        private ApplicationDbContext(DbConnection connection)
            :base (connection, true)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public static ApplicationDbContext Create(DbConnection connection)
        {
            return new ApplicationDbContext(connection);
        }

        public virtual DbSet<Customer> Customers { get; set; }
    }
}