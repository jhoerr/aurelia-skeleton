using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyTested.WebApi;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using WebApi.Models;
using WebApi.Tests.Integration.Fixtures;
using WebApi.Tests.Mock;
using Xunit;

namespace WebApi.Tests.Integration
{
    public class WhenNotUsingTheDelegatedAuthorizationHandler 
        : IClassFixture<UnauthenticatedServerFixture>
    {
        private readonly IServerBuilder _server;

        public WhenNotUsingTheDelegatedAuthorizationHandler(UnauthenticatedServerFixture fixture)
        {
            _server = fixture.Server;
        }

        [Fact]
        public void RequestIsRejectedAsUnauthorized()
        {
            _server
                .WithHttpRequestMessage(req =>
                    req.WithRequestUri("/api/customers")
                       .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }
    }

    [Collection("Database collection")]
    public class WhenUsingTheDelegatedAuthorizationHandler 
    {
        private readonly IServerBuilder _server;

        public WhenUsingTheDelegatedAuthorizationHandler(DatabaseFixture fixture)
        {
            var container = IoCConfig.CreateContainer();
            container.Options.AllowOverridingRegistrations = true;
            container.Register(() => ApplicationDbContext.Create(), Lifestyle.Scoped);
            container.Register(MockIdentity.Create, Lifestyle.Scoped);
            container.Verify();

            _server = MyWebApi.IsRegisteredWith(WebApiConfig.RegisterForIntegrationTests)
                .WithDependencyResolver(new SimpleInjectorWebApiDependencyResolver(container))
                .WithErrorDetailPolicy(IncludeErrorDetailPolicy.Always)
                .AndStartsServer();
        }

        [Fact]
        public void RequestMustHaveXTestingHeader()
        {
            _server
                .WithHttpRequestMessage(req =>
                    req.WithRequestUri("/api/customers")
                       .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void RequestIsAuthorized()
        {
            _server
                .WithHttpRequestMessage(req =>
                    req.WithRequestUri("/api/customers")
                       .WithMethod(HttpMethod.Get)
                       .WithHeader("X-Testing","true"))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK);
        }
    }
    
}