using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using AutoMapper;
using IdentityServer3.Core;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using WebApi.Models;
using WebApi.Services;

namespace WebApi
{
    public static class IoCConfig
    {
        private static readonly GenericIdentity Unauthenticated = new GenericIdentity("unauthenticated");

        public static void Register(HttpConfiguration config)
        {
            var container = CreateContainer();
            container.Verify();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        public static Container CreateContainer()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // Register type mappings
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CustomerRequest, Customer>(MemberList.Source).ReverseMap();
            });

            // Register your types, for instance using the scoped lifestyle:
            container.Register<ApplicationDbContext, ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<IIdentity>(GetRequestClaimsIdentity, Lifestyle.Scoped);

            container.Register<IGenericService<Customer, CustomerRequest>, GenericService<Customer, CustomerRequest>>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration, Assembly.Load("WebApi"));

            return container;
        }

        private static IIdentity GetRequestClaimsIdentity()
        {
            return Thread.CurrentPrincipal.Identity.IsAuthenticated 
                ? IdentityWithNameClaimType()
                : Unauthenticated;
        }

        private static IIdentity IdentityWithNameClaimType()
        {
            var identity = (ClaimsIdentity) Thread.CurrentPrincipal.Identity;
            var preferredUserName = identity.Claims.SingleOrDefault(c => c.Type == Constants.ClaimTypes.PreferredUserName);
            if (preferredUserName != null)
            {
                identity.AddClaim(new Claim(identity.NameClaimType, preferredUserName.Value));
            }
            return identity;
        }
    }
}