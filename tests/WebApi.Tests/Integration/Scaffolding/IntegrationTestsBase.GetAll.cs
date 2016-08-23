using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using MyTested.WebApi;
using Shouldly;
using Xunit;

namespace WebApi.Tests.Integration.Scaffolding
{
    // GET /api/entities
    public abstract partial class IntegrationTestsBase<TEntity, TController, TRequest>
    {
        [Fact]
        public void GetAllIsRoutedCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap($"/api/{_endpoint}")
                .WithHttpMethod(HttpMethod.Get)
                .To<TController>(c => c.Get());
        }

        [Fact]
        public void GetAllRequiresAuthentication()
        {
            Server
                .WithHttpRequestMessage(request => request
                    .WithRequestUri($"/api/{_endpoint}")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void GetAllHasCorrectStatusCode()
        {
            Server
                .WithHttpRequestMessage(request => request
                    .WithAuthorization()
                    .WithRequestUri($"/api/{_endpoint}")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public void GetAllHasReturnsExpectedCollection()
        {
            var actual = Server
                .WithHttpRequestMessage(request => request
                    .WithAuthorization()
                    .WithRequestUri($"/api/{_endpoint}")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithResponseModelOfType<IEnumerable<TEntity>>()
                .AndProvideTheModel()
                .ToList();

            actual.Count().ShouldBe(2);
            actual.ShouldContain(entity => entity.Id == Entity1.Id);
            actual.ShouldContain(entity => entity.Id == Entity2.Id);
        }
    }
}