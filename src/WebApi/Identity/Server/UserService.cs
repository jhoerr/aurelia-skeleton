using System.Linq;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using WebApi.App_Packages.IdentityServer3.AspNetIdentity;
using WebApi.Models;

namespace WebApi.Identity.Server
{
    public class UserService : AspNetIdentityUserService<ApplicationUser, string>
    {
        public UserService(ApplicationUserManager userMgr)
            : base(userMgr)
        {
        }

        protected override async Task<AuthenticateResult> PostAuthenticateLocalAsync(ApplicationUser user, SignInMessage message)
        {
            if (base.userManager.SupportsUserTwoFactor)
            {
                var id = user.Id;

                if (await userManager.GetTwoFactorEnabledAsync(id))
                {
                    var code = await this.userManager.GenerateTwoFactorTokenAsync(id, "sms");
                    var result = await userManager.NotifyTwoFactorTokenAsync(id, "sms", code);
                    if (!result.Succeeded)
                    {
                        return new AuthenticateResult(result.Errors.First());
                    }

                    var name = await GetDisplayNameForAccountAsync(id);
                    return new AuthenticateResult("~/2fa", id, name);
                }
            }

            return null;
        }
    }
}