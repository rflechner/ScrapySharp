using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapySharp.Network
{
    public class ModernScrapingBrowser : IScrapingBrowser
    {
        private readonly HttpClient httpClient;
        private readonly CookieContainer cookieContainer;

        public ModernScrapingBrowser(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            cookieContainer = new CookieContainer();
        }

        public async Task<WebResource> DownloadWebResourceAsync(Uri url, CancellationToken cancellationToken = default)
        {
            var response = await SendRequest(new HttpRequestMessage(HttpMethod.Get, url), cancellationToken).ConfigureAwait(false);
            
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            response.Headers.TryGetValues("Last-Modified", out var lastModified);
            var absoluteUrl = response.RequestMessage.RequestUri;
            
            var forceDownload = !IsCached(response.Headers.CacheControl);
            
            return new WebResource(stream, lastModified?.FirstOrDefault(), absoluteUrl, forceDownload, response.Content.Headers.ContentType.ToString());
        }
        
        private bool IsCached(CacheControlHeaderValue header)
        {
            if (header == null)
                return false;

            return !header.NoCache || !header.NoStore || header.MaxAge.GetValueOrDefault(TimeSpan.MaxValue) != TimeSpan.Zero;
        }

        public async Task<string> AjaxDownloadStringAsync(Uri url, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.TryAddWithoutValidation("X-Prototype-Version", "1.7.3");
            request.Headers.TryAddWithoutValidation("X-Requested-With", "XMLHttpRequest");
            
            var response = await SendRequest(request, cancellationToken).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            request.Content.Headers.Add("Cookies", cookieContainer.GetCookieHeader(request.RequestUri));
            
            var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

            Referer = request.RequestUri;
            
            return response;
        }

        public async Task<string> DownloadStringAsync(Uri url, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await SendRequest(request, cancellationToken).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        
        public bool AllowMetaRedirect { get; set; }
        
        public bool AutoDownloadPagesResources { get; set; }
        
        public FakeUserAgent UserAgent { get; set; }
        
        public Uri Referer { get; set; }
        
        public void SetCookies(Uri cookieUrl, string cookiesExpression)
        {
            cookieContainer.SetCookies(cookieUrl, cookiesExpression);
        }

        public async Task<string> NavigateToAsync(Uri url, HttpVerb verb, string data, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(Map(verb), url);
            request.Content = new StringContent(data);
            var response = await SendRequest(request, cancellationToken).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        private HttpMethod Map(HttpVerb verb)
        {
            switch (verb)
            {
                case HttpVerb.Get:
                    return HttpMethod.Get;
                case HttpVerb.Head:
                    return HttpMethod.Head;
                case HttpVerb.Post:
                    return HttpMethod.Post;
                case HttpVerb.Put:
                    return HttpMethod.Put;
                case HttpVerb.Delete:
                    return HttpMethod.Delete;
                case HttpVerb.Trace:
                    return HttpMethod.Trace;
                case HttpVerb.Options:
                    return HttpMethod.Options;
                default:
                    throw new ArgumentOutOfRangeException(nameof(verb), verb, null);
            }
        }

        public async Task<string> NavigateToAsync(Uri url, HttpVerb verb, NameValueCollection data, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(Map(verb), url);
            request.Content = new FormUrlEncodedContent(data.AllKeys.Select(k => new KeyValuePair<string, string>(k, data[k])));
            var response = await SendRequest(request, cancellationToken).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        
        public async Task<WebPage> NavigateToPageAsync(Uri url, HttpVerb verb = HttpVerb.Get, string data = "", string contentType = null, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(Map(verb), url);
            request.Content = new StringContent(data);
            var response = await SendRequest(request, cancellationToken).ConfigureAwait(false);
            response.Headers.TryGetValues("Last-Modified", out var lastModified);
            
            var headers = request.Headers
                .SelectMany(h => h.Value.Select(v => new KeyValuePair<string,string>(h.Key, v)))
                .ToList();
            var body = await request.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            
            var requestEncoding = GetContentEncoding(request.Content);
            var rawRequest = new RawRequest(verb.ToString(), url, request.Version, headers, body, requestEncoding);
            var responseHeaders = new NameValueCollection();
            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            var responseEncoding = GetContentEncoding(response.Content);
            var rawResponse = new RawResponse(response.Version, response.StatusCode, response.ReasonPhrase, responseHeaders, responseBody, responseEncoding);
            
            return new WebPage(this, url, AutoDownloadPagesResources, rawRequest, rawResponse, responseEncoding, true);
        }

        Encoding GetContentEncoding(HttpContent content, string defaultValue = "UTF8") => Encoding.GetEncoding(content.Headers.ContentEncoding.FirstOrDefault() ?? defaultValue);

        public Task<WebPage> NavigateToPageAsync(Uri url, HttpVerb verb, NameValueCollection data, CancellationToken cancellationToken = default) 
            => NavigateToPageAsync(url, verb, data.ToHttpVars(), cancellationToken: cancellationToken);

        public Cookie GetCookie(Uri url, string name) => cookieContainer.GetCookies(url)?[name];

        public CookieCollection GetCookieCollection(Uri url) => cookieContainer.GetCookies(url);
    }
}