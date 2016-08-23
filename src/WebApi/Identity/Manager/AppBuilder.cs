using System.Security.Claims;
using IdentityManager;
using IdentityManager.Configuration;
using Microsoft.AspNet.Identity;
using Owin;
using WebApi.Models;
using Constants = WebApi.Identity.Server.Constants;

namespace WebApi.Identity.Manager
{
    public class AppBuilder
    {
        public static void Configure(IAppBuilder idm)
        {
            var factory = new IdentityManagerServiceFactory();
            factory.Register(new Registration<ApplicationUserManager>());
            factory.Register(new Registration<ApplicationUserStore>());
            factory.Register(new Registration<ApplicationRoleManager>());
            factory.Register(new Registration<ApplicationRoleStore>());
            factory.Register(new Registration<ApplicationDbContext>());
            factory.IdentityManagerService = new Registration<IIdentityManagerService, ApplicationIdentityManagerService>();

            var securityConfiguration = new HostSecurityConfiguration()
            {
                HostAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                AdminRoleName = Constants.AdminRoleName,
                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.Name,
            };

            idm.UseIdentityManager(new IdentityManagerOptions
            {
                SecurityConfiguration = securityConfiguration,
                Factory = factory
            });

        }
    }
}