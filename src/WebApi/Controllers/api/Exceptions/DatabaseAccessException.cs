using System;

namespace WebApi.Controllers.api.Exceptions
{
    public class DatabaseAccessException : Exception
    {
        public DatabaseAccessException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}