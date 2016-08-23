using System.Net;
using System.Net.Http;
using MyTested.WebApi;
using WebApi.Models;
using WebApi.Tests.Integration.Fixtures;
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

    public class WhenUsingTheDelegatedAuthorizationHandler 
        : IClassFixture<AuthenticatedServerFixtureWithEntities<Customer>>
    {
        private readonly IServerBuilder _server;

        public WhenUsingTheDelegatedAuthorizationHandler(AuthenticatedServerFixtureWithEntities<Customer> fixture)
        {
            _server = fixture.Server;
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