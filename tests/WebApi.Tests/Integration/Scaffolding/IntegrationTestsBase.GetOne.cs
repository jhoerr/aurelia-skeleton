using System.Net;
using System.Net.Http;
using MyTested.WebApi;
using Shouldly;
using Xunit;

namespace WebApi.Tests.Integration.Scaffolding
{
    // GET api/entities/1
    public abstract partial class IntegrationTestsBase<TEntity, TController, TRequest>
    { 
        [Fact]
        public void GetOneIsRoutedCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap($"/api/{_endpoint}/{Entity1.Id}")
                .WithHttpMethod(HttpMethod.Get)
                .To<TController>(c => c.Get(Entity1.Id));
        }

        [Fact]
        public void GetOneRequiresAuthentication()
        {
            Server
                .WithHttpRequestMessage(request => request
                    .WithRequestUri($"/api/{_endpoint}/{Entity1.Id}")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void GetOneHasOkStatusCode()
        {
            Server
                .WithHttpRequestMessage(request => request
                    .WithAuthorization()
                    .WithRequestUri($"/api/{_endpoint}/{Entity1.Id}")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK);
        }

        [Fact]
        public void GetOneHasReturnsOne()
        {
            Server
                .WithHttpRequestMessage(request => request
                    .WithAuthorization()
                    .WithRequestUri($"/api/{_endpoint}/{Entity1.Id}")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithResponseModelOfType<TEntity>()
                .AndProvideTheModel()
                .Id
                .ShouldBe(Entity1.Id);
        }

        [Fact]
        public void GetOneHasNotFoundStatusCode()
        {
            Server
                .WithHttpRequestMessage(request => request
                    .WithAuthorization()
                    .WithRequestUri($"/api/{_endpoint}/999999999")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.NotFound);
        }
    }
}