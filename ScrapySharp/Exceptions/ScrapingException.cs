using System;
using System.Net;

namespace ScrapySharp.Exceptions
{
    public class ScrapingException : Exception
    {
        public ScrapingException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public ScrapingException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public ScrapingException(int statusCode, string message) : this((HttpStatusCode)statusCode, message)
        {
        }

        public HttpStatusCode StatusCode { get; }
    }
}