using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers.api.Exceptions
{
    public class HttpConflictException : HttpResponseException
    {
        public HttpConflictException()
            : base(new HttpResponseMessage(HttpStatusCode.Conflict))
        {
        }
    }
}