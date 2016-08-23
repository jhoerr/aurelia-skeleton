using Microsoft.AspNet.Identity.EntityFramework;
using WebApi.Models;

namespace WebApi.Identity.Manager
{
    public class ApplicationRoleStore : RoleStore<IdentityRole>
    {
        public ApplicationRoleStore(ApplicationDbContext ctx)
            : base(ctx)
        {
        }


    }
}