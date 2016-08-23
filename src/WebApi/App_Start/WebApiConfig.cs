using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // TODO: Add any additional configuration code.
            DoRegister(config);
        }

        public static void RegisterForIntegrationTests(HttpConfiguration config)
        {
            DoRegister(config);
            config.MessageHandlers.Add(new IntegrationTestAuthorizationHandler());
        }

        private static void DoRegister(HttpConfiguration config)
        {
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );

            // WebAPI when dealing with JSON & JavaScript!
            // Setup json serialization to serialize classes to camel (std. Json format)
            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }

    public class CustomDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory>
        GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            // inherit route attributes decorated on base class controller's actions
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(inherit: true);
        }
    }

    public class IntegrationTestAuthorizationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
        {
            var headers = request.Headers.Where(h => h.Key.Equals("X-Testing")).ToList();
            if (headers.Any() && headers.First().Value.First().Equals("true"))
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "user@user.com"),
                    new Claim(ClaimTypes.Email, "user@user.com"),
//                    new Claim(ClaimTypes.Role, "user")
                };

                var dummyPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role));

                Thread.CurrentPrincipal = dummyPrincipal;
                var httpRequestContext = request.GetRequestContext();
                httpRequestContext.Principal = dummyPrincipal;
                if (HttpContext.Current != null)
                    HttpContext.Current.User = dummyPrincipal;
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}