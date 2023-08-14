namespace Applicaton.Web.API.Extensions
{
    public class StatusCodeException : Exception
    {
        public int StatusCode { get; set; }

        public StatusCodeException() : base() { }

        public StatusCodeException(string message) : base(message) { }

        public StatusCodeException(string message, Exception e) : base(message, e) { }
    }
}
