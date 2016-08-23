using WebApi.Controllers.api;
using WebApi.Models;
using WebApi.Tests.Integration.Fixtures;
using WebApi.Tests.Integration.Scaffolding;

namespace WebApi.Tests.Integration
{
    public class GenericContollerIntegrationTests : IntegrationTestsBase<Customer, CustomersController, CustomerRequest>
    {
        public GenericContollerIntegrationTests(AuthenticatedServerFixtureWithEntities<Customer> fixture) : base(fixture)
        {
        }
    }
}