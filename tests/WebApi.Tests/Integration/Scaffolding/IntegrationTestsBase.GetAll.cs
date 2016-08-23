using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using MyTested.WebApi;
using Ploeh.AutoFixture;
using Shouldly;
using WebApi.Models;
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
            TEntity entity1 = null;
            TEntity entity2 = null;

            try
            {
                using (var context = CreateDbContext())
                {
                    var autoFixture = new Fixture();
                    entity1 = context.Set<TEntity>().Add(autoFixture.Create<TEntity>());
                    entity2 = context.Set<TEntity>().Add(autoFixture.Create<TEntity>());
                    context.SaveChanges();
                }

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
                actual.ShouldContain(entity => entity.Id == entity1.Id);
                actual.ShouldContain(entity => entity.Id == entity2.Id);

            }
            finally
            {
                CleanUp(entity1);
                CleanUp(entity2);
            }
        }
    }
}