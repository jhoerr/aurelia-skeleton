using System.Net;
using System.Net.Http;
using AutoMapper;
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
        [Fact]
        public void PutIsRoutedCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap($"/api/{_endpoint}/1")
                .WithHttpMethod(HttpMethod.Put)
                .WithJsonContent(ValidBody)
                .ToController(_endpoint);
        }

        [Fact]
        public void PutRequiresAuthentication()
        {
            Server
                .WithHttpRequestMessage(request => request
                    .WithRequestUri($"/api/{_endpoint}/1")
                    .WithMethod(HttpMethod.Put)
                    .WithJsonContent(ValidBody))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void PutHasOkStatusCode()
        {
            TEntity actual = new Fixture().Create<TEntity>();
            try
            {
                using (var dbContext = CreateDbContext())
                {
                    dbContext.Set<TEntity>().Add(actual);
                    dbContext.SaveChanges();
                }

                var putBody = JsonConvert.SerializeObject(Mapper.Map<TRequest>(actual));

                Server
                    .WithHttpRequestMessage(request => AuthorizedPut(request, actual.Id, putBody))
                    .ShouldReturnHttpResponseMessage()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithStringContent(s => actual = JsonConvert.DeserializeObject<TEntity>(s));
            }
            finally
            {
                CleanUp(actual);
            }
        }

        [Fact]
        public void PutReturnsEntity()
        {
            TEntity entity = new Fixture().Create<TEntity>();
            try
            {
                using (var dbContext = CreateDbContext())
                {
                    dbContext.Set<TEntity>().Add(entity);
                    dbContext.SaveChanges();
                }

                var putBody = JsonConvert.SerializeObject(Mapper.Map<TRequest>(entity));

                TEntity actual = null;
                Server
                    .WithHttpRequestMessage(request => AuthorizedPut(request, entity.Id, putBody))
                    .ShouldReturnHttpResponseMessage()
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithStringContent(s => actual = JsonConvert.DeserializeObject<TEntity>(s));

                actual.ShouldNotBeNull();
                actual.CreatedBy.ShouldBe(entity.CreatedBy);
                actual.CreatedOn.ShouldBe(entity.CreatedOn);
                actual.ModifiedBy.ShouldBe(MockIdentity.Username);
                actual.ModifiedOn.ShouldNotBe(entity.CreatedOn);
            }
            finally
            {
                CleanUp(entity);
            }
        }

        [Fact]
        public void PutIsIdempotent()
        {
            TEntity entity = new Fixture().Create<TEntity>();
            try
            {
                using (var dbContext = CreateDbContext())
                {
                    dbContext.Set<TEntity>().Add(entity);
                    dbContext.SaveChanges();
                }

                var putBody = JsonConvert.SerializeObject(Mapper.Map<TRequest>(entity));

                string actual1 = null;
                Server
                    .WithHttpRequestMessage(request => AuthorizedPut(request, entity.Id, putBody))
                    .ShouldReturnHttpResponseMessage()
                    .WithStringContent(s => actual1 = s);

                string actual2 = null;
                Server
                    .WithHttpRequestMessage(request => AuthorizedPut(request, entity.Id, putBody))
                    .ShouldReturnHttpResponseMessage()
                    .WithStringContent(s => actual2 = s);

                actual1.ShouldBe(actual2);
            }
            finally
            {
                CleanUp(entity);
            }
        }


        [Fact]
        public void PutValidatesModel()
        {
            Server
                .WithHttpRequestMessage(request => AuthorizedPut(request, 1, InvalidBody))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void PutRejectsInvalidId()
        {
            Server
                .WithHttpRequestMessage(request
                    => request
                        .WithAuthorization()
                        .WithRequestUri($"/api/{_endpoint}/0")
                        .WithMethod(HttpMethod.Put))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void PutRejectsEmptyModel()
        {
            Server
                .WithHttpRequestMessage(request
                    => request
                        .WithAuthorization()
                        .WithRequestUri($"/api/{_endpoint}/1")
                        .WithMethod(HttpMethod.Put))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void PutRejectsUnknownEntity()
        {
            Server
                .WithHttpRequestMessage(request => AuthorizedPut(request, 9999, ValidBody))
                .ShouldReturnHttpResponseMessage()
                .WithStatusCode(HttpStatusCode.NotFound);
        }

        [Fact]
        public void PutRejectsUniqueConstraintViolation()
        {
            TEntity entity1 = new Fixture().Create<TEntity>();
            TEntity entity2 = new Fixture().Create<TEntity>();
            try
            {
                using (var dbContext = CreateDbContext())
                {
                    dbContext.Set<TEntity>().Add(entity1);
                    dbContext.Set<TEntity>().Add(entity2);
                    dbContext.SaveChanges();
                }

                // put entity2 with entity1 content. this should trigger a unqiue constraint violation for NamedEntities
                var putBody = JsonConvert.SerializeObject(Mapper.Map<TRequest>(entity1));

                Server
                    .WithHttpRequestMessage(request
                        => AuthorizedPut(request, entity2.Id, putBody))
                    .ShouldReturnHttpResponseMessage()
                    .WithStatusCode(HttpStatusCode.Conflict);
            }
            finally
            {
                CleanUp(entity1);
                CleanUp(entity2);
            }
        }

        private IAndHttpRequestMessageBuilder AuthorizedPut(IHttpRequestMessageBuilder request, long id, string body)
        {
            return request
                .WithAuthorization()
                .WithRequestUri($"/api/{_endpoint}/{id}")
                .WithMethod(HttpMethod.Put)
                .WithJsonContent(body);
        }
    }
}