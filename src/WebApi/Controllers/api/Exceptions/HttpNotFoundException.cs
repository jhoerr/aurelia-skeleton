using System.Net;
using System.Web.Http;

namespace WebApi.Controllers.api.Exceptions
{
    public class HttpNotFoundException : HttpResponseException
    {
        public HttpNotFoundException()
            :base((HttpStatusCode) HttpStatusCode.NotFound)
        {
        }   
    }
}