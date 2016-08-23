using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using MyTested.WebApi;
using Ploeh.AutoFixture;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using WebApi.Controllers.api.Scaffolding;
using WebApi.Models;
using WebApi.Tests.Integration.Fixtures;
using WebApi.Tests.Mock;
using Xunit;
using Xunit.Abstractions;

namespace WebApi.Tests.Integration.Scaffolding
{
    [Collection("Database collection")]
    public abstract partial class IntegrationTestsBase<TEntity, TController, TRequest>
        where TEntity: Entity, new()
        where TRequest: EntityRequest, new()
        where TController: GenericApiController<TEntity, TRequest>
    {
        private readonly DatabaseFixture _fixture;
        private IServerBuilder Server { get; }
        private ApplicationDbContext CreateDbContext() => ApplicationDbContext.Create();
        private readonly string _endpoint;

        protected IntegrationTestsBase(DatabaseFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;

            Mapper.Initialize(cfg => cfg.CreateMap<TRequest, TEntity>(MemberList.Source).ReverseMap());

            output.WriteLine("Creating IoC container...");

            var container = IoCConfig.CreateContainer();
            container.Options.AllowOverridingRegistrations = true;
            container.Register(ApplicationDbContext.Create, Lifestyle.Scoped);
            container.Register(MockIdentity.Create, Lifestyle.Scoped);
            container.Verify();

            output.WriteLine("Created IoC container.");

            output.WriteLine("Starting server...");

            Server = MyWebApi.IsRegisteredWith(WebApiConfig.RegisterForIntegrationTests)
                .WithDependencyResolver(new SimpleInjectorWebApiDependencyResolver(container))
                .WithErrorDetailPolicy(IncludeErrorDetailPolicy.Always)
                .AndStartsServer();

            output.WriteLine("Started server.");

            _endpoint = typeof(TController).Name.Replace("Controller", "");
        }

        private void CleanUp(TEntity actual)
        {
            if (actual == null) return;
            using (var dbContext = CreateDbContext())
            {
                var entity = dbContext.Set<TEntity>().SingleOrDefault(e => e.Id == actual.Id);
                if (entity == null) return;
                dbContext.Set<TEntity>().Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}