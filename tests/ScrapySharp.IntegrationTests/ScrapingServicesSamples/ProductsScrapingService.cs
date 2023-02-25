using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScrapySharp.Exceptions;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using ScrapySharp.Tests.CrawlSite.Models;
using ScrapySharp.Tests.CrawlSite.Models.ValueObjects;

namespace ScrapySharp.IntegrationTests.ScrapingServicesSamples
{
    public class ProductsScrapingService
    {
        public ProductsScrapingService(IScrapingBrowser browser, Uri baseAddress)
        {
            Browser = browser;
            BaseAddress = baseAddress;
        }

        public IScrapingBrowser Browser { get; }
        public Uri BaseAddress { get; }

        protected Uri CreateUri(string uriOrPath)
        {
            if (!string.IsNullOrEmpty(uriOrPath) && uriOrPath.Contains("://"))
                return new Uri(uriOrPath);
            
            var path = string.IsNullOrWhiteSpace(uriOrPath) ? "/" : uriOrPath;

            if (!Uri.TryCreate(BaseAddress, path, out var url))
                throw new ArgumentException("invalid relative uri", path);

            return url;
        }
        
        const string productRoute = "/Home/Product/";

        public async IAsyncEnumerable<(string name, string link)> GetCategoriesLinks([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var homePage = await Browser.NavigateToPageAsync(CreateUri("/"), cancellationToken: cancellationToken);
            
            if (homePage.IsErrorPage) throw new ScrapingException(homePage.RawResponse.StatusCode, "Could not get categories list");

            var links = homePage.Html
                .CssSelect("ul#categories li a")
                .Select(a => (
                    name:a.InnerText.Trim(),
                    link:a.Attributes["href"].Value
                ));

            foreach (var categoriesLinks in links)
                yield return categoriesLinks;
        }

        public async IAsyncEnumerable<Product> GetCategoryProducts(string categoryLink, [EnumeratorCancellation]CancellationToken cancellationToken = default)
        {
            var page = await Browser.NavigateToPageAsync(CreateUri(categoryLink), cancellationToken: cancellationToken);
            if (page.IsErrorPage) throw new ScrapingException(page.RawResponse.StatusCode, "Could not get products list");

            foreach (var product in ParseProductList(page)) yield return product;

            HtmlNode next;
            var nextLink = string.Empty;
            
            do
            {
                next = page.Html
                    .CssSelect("nav[aria-label=pages] ul.pagination li.page-item a.page-link")
                    .LastOrDefault(a => a.InnerText.Trim().Equals("Next", StringComparison.InvariantCultureIgnoreCase) && !a.ParentNode.HasClass("disabled"));

                var link = next?.Attributes["href"].Value.CleanInnerText();
                if (link == nextLink) throw new PageParsingException("Cannot determine next product page link", page.Html);

                nextLink = link;
                if (string.IsNullOrWhiteSpace(nextLink)) break;
                
                page = await Browser.NavigateToPageAsync(CreateUri(nextLink), cancellationToken: cancellationToken);
                if (page.IsErrorPage) throw new ScrapingException(page.RawResponse.StatusCode, "Could not get products list");

                var productList = ParseProductList(page).ToImmutableArray();
                if (productList.IsEmpty) break;
                foreach (var product in productList) yield return product;
                
            } while (next != null);
        }
        
        private static IEnumerable<Product> ParseProductList(WebPage page)
        {
            return
                from tr in page.Html.CssSelect("div#products table tbody tr")
                    let cells = tr.Elements("td").ToImmutableArray()
                where cells.Length >= 3
                    let priceText = cells[1].InnerText.CleanInnerText().Replace("likes", string.Empty).Trim()
                    let price = FluentParsing.TryParseInt(priceText)
                where price.successfullyParsed
                    let name = cells[0].InnerText.CleanInnerText()
                    let link = cells[2].Element("a").Attributes["href"].Value
                where link.StartsWith(productRoute)
                    let productId = ProductId.Parse(link.Substring(productRoute.Length))
                select new Product(productId, name, price.value);
        }
        
    }
}