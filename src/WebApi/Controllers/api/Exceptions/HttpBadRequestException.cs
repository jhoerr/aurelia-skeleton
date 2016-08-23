using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers.api.Exceptions
{
    public class HttpBadRequestException : HttpResponseException
    {
        public HttpBadRequestException()
            :base(new HttpResponseMessage(HttpStatusCode.BadRequest))
        {
        }
    }
}