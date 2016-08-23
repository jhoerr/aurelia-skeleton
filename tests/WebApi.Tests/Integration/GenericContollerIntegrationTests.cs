using WebApi.Controllers.api;
using WebApi.Models;
using WebApi.Tests.Integration.Fixtures;
using WebApi.Tests.Integration.Scaffolding;
using Xunit.Abstractions;

namespace WebApi.Tests.Integration
{
    public class GenericContollerIntegrationTests : IntegrationTestsBase<Customer, CustomersController, CustomerRequest>
    {
        public GenericContollerIntegrationTests(DatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
        {
        }
    }
}