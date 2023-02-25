using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using ScrapySharp.Cache;
using ScrapySharp.Extensions;
using System.Linq;
using System.Threading.Tasks;
using ScrapySharp.Html;
using ScrapySharp.Html.Forms;

namespace ScrapySharp.Network
{
    public sealed class WebPage
    {
        private readonly IScrapingBrowser browser;
        private readonly Uri absoluteUrl;
        private readonly bool autoDetectCharsetEncoding;
        private string content;
        private readonly List<WebResource> resources;
        private HtmlNode html;
        private string baseUrl;

        private static readonly Dictionary<string, string> resourceTags = new Dictionary<string, string> 
            {
                {"img", "src"},
                {"script", "src"},
                {"link", "href"},
            };
        public Encoding Encoding { get; private set; }

        public WebPage(IScrapingBrowser browser, Uri absoluteUrl, bool autoDownloadPagesResources, RawRequest rawRequest, RawResponse rawResponse, 
            Encoding encoding, bool autoDetectCharsetEncoding)
        {
            this.browser = browser;
            this.absoluteUrl = absoluteUrl;
            this.RawRequest = rawRequest;
            this.RawResponse = rawResponse;
            this.autoDetectCharsetEncoding = autoDetectCharsetEncoding;
            Encoding = encoding;

            content = Encoding.GetString(rawResponse.Body);
            resources = new List<WebResource>();

            LoadHtml();

            if (autoDownloadPagesResources)
            {
                LoadBaseUrl();
                // TODO: lazy download
                DownloadResourcesAsync().GetAwaiter().GetResult();
            }
        }

        private void LoadHtml()
        {
            try
            {
                html = content.ToHtmlNode();
                if (autoDetectCharsetEncoding)
                {
                    var charset = html.Descendants("meta").Select(meta => meta.GetAttributeValue("charset", string.Empty).Trim())
                        .FirstOrDefault(v => !string.IsNullOrEmpty(v));
                    if (charset == null)
                    {
                        // Parse content-type too.
                        var contentType = html.Descendants("meta").FirstOrDefault(m => m.GetAttributeValue("http-equiv") == "content-type");
                        if (contentType != null)
                        {
                            var contentTypeContent = contentType.GetAttributeValue("content");
                            int posContentType = contentTypeContent.IndexOf("charset=", StringComparison.Ordinal);
                            if (posContentType != -1)
                            {
                                charset = contentTypeContent.Substring(posContentType + "charset=".Length);
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(charset))
                    {
                        Encoding = Encoding.GetEncoding(charset);
                        content = Encoding.GetString(RawResponse.Body);
                        html = content.ToHtmlNode();
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public bool AutoDetectCharsetEncoding => autoDetectCharsetEncoding;

        public RawRequest RawRequest { get; }

        public RawResponse RawResponse { get; }

        public bool IsErrorPage => RawResponse.StatusCode < 200 || RawResponse.StatusCode >= 300;
        
        public IEnumerable<HtmlNode> Find(string tag, By by)
        {
            return @by.CreateElementFinder(html, tag).FindElements();
        }

        public IEnumerable<HyperLink> FindLinks(By by)
        {
            return Find("a", by).Select(a => new HyperLink(this, a));
        }

        public PageWebForm FindForm(string name)
        {
            var node = (from n in Html.Descendants("form")
                        let formName = n.GetAttributeValue("name", string.Empty)
                        where formName == name
                        select n).FirstOrDefault();

            return node == null ? null : new PageWebForm(node, browser);
        }

        public PageWebForm FindFormById(string id)
        {
            var node = Html.Descendants("form").FirstOrDefault(f => f.Id == id);
            return node == null ? null : new PageWebForm(node, browser);
        }

        private void LoadBaseUrl()
        {
            var baseAttr = html.Descendants("base").Where(e => e.Attributes.Any(a => a.Name == "href"))
                .Select(e => e.Attributes["href"].Value).FirstOrDefault();

            if (baseAttr != null)
            {
                baseUrl = baseAttr;
                return;
            }

            baseUrl = string.Format("{0}://{1}", absoluteUrl.Scheme, absoluteUrl.Host);
            if (!absoluteUrl.IsDefaultPort)
                baseUrl += ":" + absoluteUrl.Port;
        }

        public override string ToString()
        {
            return content;
        }

        public static implicit operator string(WebPage page)
        {
            return page.content;
        }

        private async Task DownloadResourcesAsync()
        {
            var resourceUrls = GetResourceUrls();

            foreach (var resourceUrl in resourceUrls)
            {
                var url = GetFullResourceUrl(resourceUrl, absoluteUrl);

                if (WebResourceStorage.Current.Exists(url.ToString()))
                    continue;

                try
                {
                    WebResource resource = await browser.DownloadWebResourceAsync(url).ConfigureAwait(false);
                    resources.Add(resource);
                    if (!resource.ForceDownload || !string.IsNullOrEmpty(resource.LastModified))
                        WebResourceStorage.Current.Save(resource);
                }
                catch
                {
                    
                }
            }
        }

        private Uri GetFullResourceUrl(string resourceUrl, Uri root)
        {
            Uri result;
            Uri.TryCreate(resourceUrl, UriKind.RelativeOrAbsolute, out result);
            Uri url;

            if (!result.IsAbsoluteUri)
            {
                if (resourceUrl.StartsWith("/") || resourceUrl.StartsWith("./") || resourceUrl.StartsWith("../"))
                {
                    url = baseUrl != null ? baseUrl.CombineUrl(resourceUrl) : root.Combine(resourceUrl);
                }
                else
                {
                    var path = string.Join("/", root.Segments.Take(root.Segments.Length - 1).Skip(1));
                    url = baseUrl != null ? baseUrl.CombineUrl(path).Combine(resourceUrl) : root.Combine(resourceUrl);
                }
            }
            else
                url = new Uri(resourceUrl);
            return url;
        }

        public List<string> GetResourceUrls()
        {
            var resourceUrls = new List<string>();

            foreach (var resourceTag in resourceTags)
            {
                var sources = html.Descendants(resourceTag.Key)
                    .Where(e => e.Attributes.Any(a => a.Name == resourceTag.Value))
                    .Select(e => e.Attributes[resourceTag.Value].Value).ToArray();
                resourceUrls.AddRange(sources);
            }
            return resourceUrls;
        }

        public IScrapingBrowser Browser
        {
            get { return browser; }
        }

        public Uri AbsoluteUrl
        {
            get { return absoluteUrl; }
        }

        public string Content
        {
            get { return content; }
        }

        public List<WebResource> Resources
        {
            get { return resources; }
        }

        public HtmlNode Html
        {
            get { return html; }
        }

        public string BaseUrl
        {
            get { return baseUrl; }
        }

        private static readonly Regex urlInCssRegex = new Regex(@"url \s* [(] \s* (?<url>[^)\r\n]+) \s* [)]", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        public async Task SaveSnapshot(string path)
        {
            if (!browser.AutoDownloadPagesResources)
                await DownloadResourcesAsync().ConfigureAwait(false);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
                Directory.GetFiles(path).ToList().ForEach(File.Delete);
            
            foreach (var resource in Resources)
            {
                var guid = Guid.NewGuid();
                resource.Content.Position = 0;
                var fileName = guid.ToString("N");
                
                RewriteHtml(resource, fileName);

                if (!string.IsNullOrEmpty(resource.ContentType) && resource.ContentType.EndsWith("css", StringComparison.InvariantCultureIgnoreCase))
                {
                    var textContent = resource.GetTextContent();
                    textContent = await RewriteCssUrls(path, textContent, resource.AbsoluteUrl.ToString()).ConfigureAwait(false);
                    File.WriteAllText(Path.Combine(path, fileName), textContent);
                }
                else
                {
                    await using var dumpFile = File.OpenWrite(Path.Combine(path, fileName));
                    await resource.Content.CopyToAsync(dumpFile).ConfigureAwait(false);
                }
            }

            var outerHtml = await RewriteCssUrls(path, Html.OuterHtml, AbsoluteUrl.ToString()).ConfigureAwait(false);
            File.WriteAllText(Path.Combine(path, "page.html"), outerHtml);
        }

        private void RewriteHtml(WebResource resource, string fileName)
        {
            foreach (var resourceTag in resourceTags)
            {
                var nodes = html.Descendants(resourceTag.Key)
                    .Where(
                        e =>
                        e.Attributes.Any(a => a.Name == resourceTag.Value) &&
                        resource.AbsoluteUrl.ToString().EndsWith(e.Attributes[resourceTag.Value].Value))
                    .ToArray();

                foreach (var node in nodes)
                {
                    node.SetAttributeValue(resourceTag.Value, fileName);
                }
            }
        }

        private async Task<string> RewriteCssUrls(string path, string textContent, string rootUrl)
        {
            var match = urlInCssRegex.Match(textContent);
            while (match.Success)
            {
                var imageId = Guid.NewGuid().ToString("N");
                var url = match.Groups["url"].Value;

                var parts = rootUrl.Split('/');
                var leftPart = string.Join("/", parts.Take(parts.Length - 1));

                try
                {
                    var image = await browser.DownloadWebResourceAsync(GetFullResourceUrl(url, new Uri(leftPart)));
                    
                    await using var dumpFile = File.OpenWrite(Path.Combine(path, imageId));
                    await image.Content.CopyToAsync(dumpFile).ConfigureAwait(false);
                }
                catch
                {

                }

                textContent = textContent.Replace(url, imageId);
                match = match.NextMatch();
            }
            return textContent;
        }
    }
}