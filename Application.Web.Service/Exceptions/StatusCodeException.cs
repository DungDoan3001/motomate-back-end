using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Service.Exceptions
{
    public class StatusCodeException : Exception
    {
        public int StatusCode { get; set; }

        public StatusCodeException() : base() { }

        public StatusCodeException(string message) : base(message) { }

        public StatusCodeException(string message, Exception e) : base(message, e) { }

        public StatusCodeException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public StatusCodeException(string message, int statusCode, Exception e) : base(message, e)
        {
            StatusCode = statusCode;
        }
    }
}
