using System.Web.Http;
using WebApi.Controllers.api.Scaffolding;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers.api
{
    [Authorize]
    [RoutePrefix("api/customers")]
    public class CustomersController : GenericApiController<Customer, CustomerRequest>
    {
        public CustomersController(IGenericService<Customer, CustomerRequest> service):base(service)
        {
        }
    }
}
