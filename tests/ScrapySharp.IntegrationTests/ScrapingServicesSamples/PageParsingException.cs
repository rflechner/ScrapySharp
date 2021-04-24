using System;
using HtmlAgilityPack;

namespace ScrapySharp.IntegrationTests.ScrapingServicesSamples
{
    public class PageParsingException : Exception
    {
        public PageParsingException(string message, HtmlNode html) : base(message)
        {
            Html = html;
        }

        public HtmlNode Html { get; }
    }
}