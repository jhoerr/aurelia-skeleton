using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityManager;
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

        public override async Task<IdentityManagerResult<UserDetail>> GetUserAsync(string subject)
        {
            var user = await base.GetUserAsync(subject);
            var roles = await userManager.GetRolesAsync(subject);
            var claims = user.Result.Claims.ToList();
            claims.AddRange(roles.Select(role => new ClaimValue() { Type = RoleClaimType, Value = role }));
            user.Result.Claims = claims;
            return user;
        }

        public override async Task<IdentityManagerResult> AddUserClaimAsync(string subject, string type, string value)
        {
            if (type == RoleClaimType)
            {
                var status = await userManager.AddToRoleAsync(ConvertUserSubjectToKey(subject), value);
                return status.Succeeded 
                    ? IdentityManagerResult.Success 
                    : new IdentityManagerResult<CreateResult>(status.Errors.ToArray());
            }

            return await base.AddUserClaimAsync(subject, type, value);
        }

        public override async Task<IdentityManagerResult> RemoveUserClaimAsync(string subject, string type, string value)
        {
            if (type == RoleClaimType)
            {
                var status = await userManager.RemoveFromRoleAsync(ConvertUserSubjectToKey(subject), value);
                return status.Succeeded
                    ? IdentityManagerResult.Success
                    : new IdentityManagerResult<CreateResult>(status.Errors.ToArray());
            }

            return await base.RemoveUserClaimAsync(subject, type, value);
        }
    }
}