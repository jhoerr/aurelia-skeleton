using Microsoft.AspNet.Identity.EntityFramework;
using WebApi.Models;

namespace WebApi.Identity.Manager
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext ctx)
            : base(ctx)
        {
        }
    }
}