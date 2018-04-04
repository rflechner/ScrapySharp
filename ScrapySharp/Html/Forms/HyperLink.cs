using System;
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

        public WebPage Click()
        {
            var href = node.GetAttributeValue("href", string.Empty);
            if (string.IsNullOrWhiteSpace(href))
                return null;

            Uri url;
            if (Uri.TryCreate(href, UriKind.Absolute, out url))
                return page.Browser.NavigateToPage(url, HttpVerb.Get, string.Empty);

            url = page.Browser.Referer.Combine(href);
            return page.Browser.NavigateToPage(url, HttpVerb.Get, string.Empty);
        }
    }
}