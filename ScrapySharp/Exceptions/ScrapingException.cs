using System;

namespace ScrapySharp.Exceptions
{
    public class ScrapingException : Exception
    {
        public ScrapingException(string message) : base(message)
        {
        }

        public ScrapingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}