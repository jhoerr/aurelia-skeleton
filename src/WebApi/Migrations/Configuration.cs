using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApi.Identity.Manager;
using WebApi.Identity.Server;
using WebApi.Models;

namespace WebApi.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            EnsureDefaultRolesAndUsersCreated(context);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        private static void EnsureDefaultRolesAndUsersCreated(ApplicationDbContext context)
        {
            // Create default roles
            var userRole = EnsureRoleCreated(context, Constants.UserRoleName);
            var adminRole = EnsureRoleCreated(context, Constants.AdminRoleName);

            // Create default users
            EnsureUserCreated(context, "admin@example.com", "admin@example.com", "@bcd1234!", adminRole, userRole);
            EnsureUserCreated(context, "user@example.com", "user@example.com", "@bcd0987!", userRole);
        }

        private static IdentityRole EnsureRoleCreated(ApplicationDbContext context, string roleName)
        {
            var roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));
            var role = roleManager.FindByNameAsync(roleName).Result;
            if (role == null)
            {
                roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
                role = roleManager.FindByNameAsync(roleName).Result;
            }
            return role;
        }

        private static void EnsureUserCreated(ApplicationDbContext context, string username, string email, string password, params IdentityRole[] roles)
        {
            var userManager = new ApplicationUserManager(new ApplicationUserStore(context));
            var user = userManager.FindByNameAsync(username).Result;
            if (user == null)
            {
                var applicationUser = new ApplicationUser() { UserName = username, Email = email, EmailConfirmed = true };
                userManager.CreateAsync(applicationUser, password).Wait();
                user = userManager.FindByNameAsync(username).Result;
            }

            foreach (var role in roles)
            {
                if (!user.Roles.Any(r => r.RoleId == role.Id))
                {
                    userManager.AddClaimAsync(user.Id, new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role.Name)).Wait();
                }
            }
        }
    }
}
