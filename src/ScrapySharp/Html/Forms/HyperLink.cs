using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace ScrapySharp.Html.Forms
{
    public class HyperLink
    {
        private readonly WebPage page;
        private readonly HtmlNode node;

        internal HyperLink(WebPage page, HtmlNode node)
        {
            this.page = page;
            this.node = node;
        }

        public string Text
        {
            get { return node.InnerText; }
        }

        public Task<WebPage> ClickAsync()
        {
            var href = node.GetAttributeValue("href", string.Empty);
            if (string.IsNullOrWhiteSpace(href))
                return null;

            if (Uri.TryCreate(href, UriKind.Absolute, out var url))
                return page.Browser.NavigateToPageAsync(url, HttpMethod.Get, string.Empty);

            return page.Browser.NavigateToPageAsync(page.Browser.Referer.Combine(href), HttpMethod.Get, string.Empty);
        }
    }
}