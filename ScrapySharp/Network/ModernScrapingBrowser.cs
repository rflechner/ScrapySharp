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
using ScrapySharp.Exceptions;

namespace ScrapySharp.Network
{
    public class ModernScrapingBrowser : IScrapingBrowser
    {
        private static readonly HttpMessageHandler DefaultHandler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip };

        private readonly HttpClient httpClient;
        private readonly CookieContainer cookieContainer;

        public ModernScrapingBrowser(string botName)
            : this(new HttpClient(DefaultHandler), new UserAgent(botName, botName))
        {
        }

        public ModernScrapingBrowser(HttpClient httpClient, string botName)
            : this(httpClient, new UserAgent(botName, botName))
        {
        }
        
        public ModernScrapingBrowser(HttpClient httpClient, UserAgent userAgent)
        {
            this.httpClient = httpClient;
            cookieContainer = new CookieContainer();
            UserAgent = userAgent;
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
            if (UserAgent != null)
                request.Headers.UserAgent.ParseAdd(UserAgent.Value);
            
            //request.Headers.Add("Cookies", cookieContainer.GetCookieHeader(request.RequestUri));

            request.Headers.Referrer = Referer;
            
            var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

            Referer = request.RequestUri;
            
            return response;
        }

        public async Task<string> DownloadStringAsync(Uri url, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await SendRequest(request, cancellationToken).ConfigureAwait(false);

            var rawBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            
            if (!response.IsSuccessStatusCode)
                throw new ScrapingException(response.StatusCode, rawBody);

            return rawBody;
        }
        
        public bool AllowMetaRedirect { get; set; }
        
        public bool AutoDownloadPagesResources { get; set; }
        
        public UserAgent UserAgent { get; set; }
        
        public Uri Referer { get; set; }
        
        public void SetCookies(Uri cookieUrl, string cookiesExpression)
        {
            cookieContainer.SetCookies(cookieUrl, cookiesExpression);
        }

        public async Task<string> NavigateToAsync(Uri url, HttpMethod verb, string data, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(verb, url);
            request.Content = new StringContent(data);
            var response = await SendRequest(request, cancellationToken).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public async Task<string> NavigateToAsync(Uri url, HttpMethod verb, NameValueCollection data, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(verb, url);
            request.Content = new FormUrlEncodedContent(data.AllKeys.Select(k => new KeyValuePair<string, string>(k, data[k])));
            var response = await SendRequest(request, cancellationToken).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        
        public async Task<WebPage> NavigateToPageAsync(Uri url, HttpMethod verb = null, string data = null, string contentType = null, CancellationToken cancellationToken = default)
        {
            if (verb == null)
                verb = HttpMethod.Get;
            
            var request = new HttpRequestMessage(verb, url);

            var headers = request.Headers
                .SelectMany(h => h.Value.Select(v => new KeyValuePair<string,string>(h.Key, v)))
                .ToList();

            RawRequest rawRequest;
            
            if (data != null)
            {
                request.Content = new StringContent(data);
                if (!string.IsNullOrWhiteSpace(contentType) && MediaTypeHeaderValue.TryParse(contentType, out var contentTypeHeader))
                {
                    request.Content.Headers.ContentType = contentTypeHeader;
                }
                var body = await request.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                var requestEncoding = GetContentEncoding(request.Content);
                rawRequest = new RawRequest(verb.ToString(), url, request.Version, headers, body, requestEncoding);
            }
            else
            {
                rawRequest = new RawRequest(verb.ToString(), url, request.Version, headers);
            }

            var response = await SendRequest(request, cancellationToken).ConfigureAwait(false);

            var responseHeaders = response.Headers.Select(h => new Header(h.Key, h.Value.ToArray())).ToArray();

            var responseBody = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            var responseEncoding = GetContentEncoding(response.Content);
            
            var rawResponse = new RawResponse(response.Version, response.StatusCode, response.ReasonPhrase, responseHeaders, responseBody, responseEncoding);
            
            return new WebPage(this, url, AutoDownloadPagesResources, rawRequest, rawResponse, responseEncoding, true);
        }

        Encoding GetContentEncoding(HttpContent content, Encoding defaultEncoding = null)
        {
            defaultEncoding ??= Encoding.UTF8;
            
            var name = content.Headers.ContentEncoding.FirstOrDefault();
            return string.IsNullOrWhiteSpace(name) ? defaultEncoding : Encoding.GetEncoding(name);
        }

        public Task<WebPage> NavigateToPageAsync(Uri url, HttpMethod verb, NameValueCollection data, CancellationToken cancellationToken = default) 
            => NavigateToPageAsync(url, verb, data.ToHttpVars(), cancellationToken: cancellationToken);

        public Cookie GetCookie(Uri url, string name) => cookieContainer.GetCookies(url)?[name];

        public CookieCollection GetCookieCollection(Uri url) => cookieContainer.GetCookies(url);

        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}