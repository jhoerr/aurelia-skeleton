using System.Net;
using System.Net.Http;
using MyTested.WebApi;
using MyTested.WebApi.Builders.Contracts.HttpRequests;
using Ploeh.AutoFixture;
using Xunit;

namespace WebApi.Tests.Integration.Scaffolding
{
    public abstract partial class IntegrationTestsBase<TEntity, TController, TRequest>
    {
        [Fact]
        public void DeleteIsRoutedCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap($"/api/{_endpoint}/1")
                .WithHttpMethod(HttpMethod.Delete)
                .WithJsonContent(ValidBody)
                .ToController(_endpoint);
        }

        [Fact]
        public void DeleteRequiresAuthentication()
        {
            Server
                .WithHttpRequestMessage(request => request
                    .WithRequestUri($"/api/{_endpoint}/1")
                    .WithMethod(HttpMethod.Delete))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void DeleteHasNoContentStatusCode()
        {
            TEntity actual = new Fixture().Create<TEntity>();
            try
            {
                using (var dbContext = CreateDbContext())
                {
                    dbContext.Set<TEntity>().Add(actual);
                    dbContext.SaveChanges();
                }

                Server
                    .WithHttpRequestMessage(request => AuthorizedDelete(request, actual.Id))
                    .ShouldReturnHttpResponseMessage()
                    .WithStatusCode(HttpStatusCode.NoContent);
            }
            finally
            {
                CleanUp(actual);
            }
        }

        [Fact]
        public void DeleteRejectsInvalidId()
        {
            Server
                .WithHttpRequestMessage(request => AuthorizedDelete(request, 0))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void DeleteRejectsUnknownEntity()
        {
            Server
                .WithHttpRequestMessage(request => AuthorizedDelete(request, 9999))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        private IAndHttpRequestMessageBuilder AuthorizedDelete(IHttpRequestMessageBuilder request, long id)
        {
            return request
                .WithAuthorization()
                .WithRequestUri($"/api/{_endpoint}/{id}")
                .WithMethod(HttpMethod.Delete);
        }
    }
}