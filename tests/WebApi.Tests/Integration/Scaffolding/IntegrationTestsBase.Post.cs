using System.Net;
using System.Net.Http;
using MyTested.WebApi;
using MyTested.WebApi.Builders.Contracts.HttpRequests;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using Shouldly;
using WebApi.Tests.Mock;
using Xunit;

namespace WebApi.Tests.Integration.Scaffolding
{
    public abstract partial class IntegrationTestsBase<TEntity, TController, TRequest>
    {
        private string ValidBody = JsonConvert.SerializeObject(new Fixture().Create<TEntity>());
        private string InvalidBody = JsonConvert.SerializeObject(new TEntity());

        [Fact]
        public void PostIsRoutedCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap($"/api/{_endpoint}")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(ValidBody)
                .ToController(_endpoint);
        }
        
        [Fact]
        public void PostRequiresAuthentication()
        {
            Server
                .WithHttpRequestMessage(request => request
                    .WithRequestUri($"/api/{_endpoint}")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonContent(ValidBody))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void PostHasCreatedStatusCode()
        {
            TEntity actual = null;
            try
            {
                Server
                    .WithHttpRequestMessage(request => AuthorizedPost(request, ValidBody))
                    .ShouldReturnHttpResponseMessage()
                    .WithStatusCode(HttpStatusCode.Created)
                    .WithStringContent(s => actual = JsonConvert.DeserializeObject<TEntity>(s));
            }
            finally
            {
                CleanUp(actual);
            }
        }

        [Fact]
        public void PostReturnsEntity()
        {
            TEntity actual = null;
            try
            {
                Server
                    .WithHttpRequestMessage(request => AuthorizedPost(request, ValidBody))
                    .ShouldReturnHttpResponseMessage()
                    .WithStringContent(s => actual = JsonConvert.DeserializeObject<TEntity>(s));

                actual.ShouldNotBeNull();
                actual.CreatedBy.ShouldBe(MockIdentity.Username);
                actual.ModifiedBy.ShouldBe(MockIdentity.Username);
            }
            finally
            {
                CleanUp(actual);
            }
        }

        [Fact]
        public void PostValidatesModel()
        {
            Server
                .WithHttpRequestMessage(request => AuthorizedPost(request, InvalidBody))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void PostRejectsEmptyModel()
        {
            Server
                .WithHttpRequestMessage(request
                    => request
                        .WithAuthorization()
                        .WithRequestUri($"/api/{_endpoint}")
                        .WithMethod(HttpMethod.Post))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void PostRejectsDuplicate()
        {
            TEntity actual = null;
            try
            {
                Server
                    .WithHttpRequestMessage(request => AuthorizedPost(request, ValidBody))
                    .ShouldReturnHttpResponseMessage()
                    .WithStatusCode(HttpStatusCode.Created)
                    .WithStringContent(s => actual = JsonConvert.DeserializeObject<TEntity>(s));

                Server
                    .WithHttpRequestMessage(request => AuthorizedPost(request, ValidBody))
                    .ShouldReturnHttpResponseMessage()
                    .WithStatusCode(HttpStatusCode.Conflict);
            }
            finally
            {
                CleanUp(actual);
            }
        }

        private IAndHttpRequestMessageBuilder AuthorizedPost(IHttpRequestMessageBuilder request, string body)
        {
            return request
                .WithAuthorization()
                .WithRequestUri($"/api/{_endpoint}")
                .WithMethod(HttpMethod.Post)
                .WithJsonContent(body);
        }
    }
}