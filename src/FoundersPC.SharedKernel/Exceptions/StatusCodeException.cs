using System;
using System.Net;

namespace FoundersPC.SharedKernel.Exceptions
{
    public class StatusCodeException : Exception
    {
        public StatusCodeException(HttpStatusCode statusCode, string message) : base(message) => StatusCode = statusCode;

        public StatusCodeException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException) => StatusCode = statusCode;

        public HttpStatusCode StatusCode { get; }
    }
}