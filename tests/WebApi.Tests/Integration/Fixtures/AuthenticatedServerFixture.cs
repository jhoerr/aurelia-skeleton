using System;
using System.Data.Common;
using System.Web.Http;
using MyTested.WebApi;
using Ploeh.AutoFixture;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using WebApi.Models;
using WebApi.Tests.Mock;

namespace WebApi.Tests.Integration.Fixtures
{
    public class AuthenticatedServerFixtureWithEntities<T> : IDisposable
        where T:Entity 
    {
        public readonly ApplicationDbContext DbContext;
        public readonly DbConnection DbConnection;
        public IServerBuilder Server { get; }
        public T Entity1 { get; }
        public T Entity2 { get; }

        public AuthenticatedServerFixtureWithEntities()
        {
            var fixture = new Fixture();

            Effort.Provider.EffortProviderConfiguration.RegisterProvider();

            DbConnection = Effort.DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            DbContext = ApplicationDbContext.Create(DbConnection);
            Entity1 = DbContext.Set<T>().Add(fixture.Create<T>());
            Entity2 = DbContext.Set<T>().Add(fixture.Create<T>());
            DbContext.SaveChanges();

            var container = IoCConfig.CreateContainer();
            container.Options.AllowOverridingRegistrations = true;
            container.Register(() => ApplicationDbContext.Create(DbConnection), Lifestyle.Scoped);
            container.Register(MockIdentity.Create, Lifestyle.Scoped);
            container.Verify();
            
            Server = MyWebApi.IsRegisteredWith(WebApiConfig.RegisterForIntegrationTests)
                .WithDependencyResolver(new SimpleInjectorWebApiDependencyResolver(container))
                .WithErrorDetailPolicy(IncludeErrorDetailPolicy.Always)
                .AndStartsServer();
        }

        public void Dispose()
        {
        }
    }

}
