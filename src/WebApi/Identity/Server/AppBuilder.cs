using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Owin;
using Serilog;
using WebApi.Identity.Manager;
using WebApi.Models;

namespace WebApi.Identity.Server
{
    public static class AppBuilder
    {
        public static void Configure(IAppBuilder core)
        {
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
             .WriteTo.Trace()
             .CreateLogger();

            var factory = new IdentityServerServiceFactory()
                .UseInMemoryClients(Clients.Get())
                .UseInMemoryScopes(Scopes.Get());

            factory.CorsPolicyService = new Registration<ICorsPolicyService>(new DefaultCorsPolicyService {AllowAll = true});
            factory.UserService = new Registration<IUserService, UserService>();
            factory.Register(new Registration<ApplicationUserManager>());
            factory.Register(new Registration<ApplicationUserStore>());
            factory.Register(new Registration<ApplicationDbContext>());

            var options = new IdentityServerOptions
            {
                SiteName = "IdentityServer3",
                SigningCertificate = Certificate.Get(),
                RequireSsl = true,
                Factory = factory,
                EventsOptions = new EventsOptions
                {
                    RaiseSuccessEvents = true,
                    RaiseErrorEvents = true,
                    RaiseFailureEvents = true,
                    RaiseInformationEvents = true
                },
                AuthenticationOptions = new AuthenticationOptions
                {
                    EnablePostSignOutAutoRedirect = true,
                    
                    
                },
                
            };

            core.UseIdentityServer(options);
        }
    }
}