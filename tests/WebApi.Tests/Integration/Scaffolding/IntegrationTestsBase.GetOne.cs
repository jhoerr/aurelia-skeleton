using System.Net;
using System.Net.Http;
using MyTested.WebApi;
using Ploeh.AutoFixture;
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
                .ShouldMap($"/api/{_endpoint}/1")
                .WithHttpMethod(HttpMethod.Get)
                .To<TController>(c => c.Get(1));
        }

        [Fact]
        public void GetOneRequiresAuthentication()
        {
            Server
                .WithHttpRequestMessage(request => request
                    .WithRequestUri($"/api/{_endpoint}/1")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void GetOneHasOkStatusCode()
        {
            TEntity entity = null;
            try
            {
                using (var context = CreateDbContext())
                {
                    var autoFixture = new Fixture();
                    entity = context.Set<TEntity>().Add(autoFixture.Create<TEntity>());
                    context.SaveChanges();
                }
                Server
                .WithHttpRequestMessage(request => request
                    .WithAuthorization()
                    .WithRequestUri($"/api/{_endpoint}/{entity.Id}")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.OK);
            }
            finally
            {
                CleanUp(entity);
            }
        }

        [Fact]
        public void GetOneHasReturnsOne()
        {
            TEntity entity = null;
            try
            {
                using (var context = CreateDbContext())
                {
                    var autoFixture = new Fixture();
                    entity = context.Set<TEntity>().Add(autoFixture.Create<TEntity>());
                    context.SaveChanges();
                }

                Server
                    .WithHttpRequestMessage(request => request
                        .WithAuthorization()
                        .WithRequestUri($"/api/{_endpoint}/{entity.Id}")
                        .WithMethod(HttpMethod.Get))
                    .ShouldReturnHttpResponseMessage()
                    .WithResponseModelOfType<TEntity>()
                    .AndProvideTheModel()
                    .Id
                    .ShouldBe(entity.Id);

            }
            finally
            {
                CleanUp(entity);
            }
        }

        [Fact]
        public void GetOneHasNotFoundStatusCode()
        {
            Server
                .WithHttpRequestMessage(request => request
                    .WithAuthorization()
                    .WithRequestUri($"/api/{_endpoint}/1")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.NotFound);
        }
    }
}