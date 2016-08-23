using System.Security.Claims;
using IdentityManager.AspNetIdentity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApi.Models;

namespace WebApi.Identity.Manager
{
    public class ApplicationIdentityManagerService : AspNetIdentityManagerService<ApplicationUser, string, IdentityRole, string>
    {
        public ApplicationIdentityManagerService(ApplicationUserManager userMgr, ApplicationRoleManager roleMgr)
            : base(userMgr, roleMgr)
        {
            RoleClaimType = ClaimTypes.Role;
        }
    }
}