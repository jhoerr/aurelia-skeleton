using System;
using System.Web.Http;
using MyTested.WebApi;
using SimpleInjector.Integration.WebApi;

namespace WebApi.Tests.Integration.Fixtures
{
    public class UnauthenticatedServerFixture :IDisposable
    {
        public IServerBuilder Server { get; }

        public UnauthenticatedServerFixture()
        {
            var container = IoCConfig.CreateContainer();

            Server = MyWebApi.IsRegisteredWith(WebApiConfig.Register)
                .WithDependencyResolver(new SimpleInjectorWebApiDependencyResolver(container))
                .WithErrorDetailPolicy(IncludeErrorDetailPolicy.Always)
                .AndStartsServer();
        }

        public void Dispose()
        {
        }
    }
}