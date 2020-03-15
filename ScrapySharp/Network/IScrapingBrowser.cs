using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapySharp.Network
{
    public interface IScrapingBrowser
    {
        Task<WebResource> DownloadWebResourceAsync(Uri url, CancellationToken cancellationToken = default);
        
        Task<string> AjaxDownloadStringAsync(Uri url, CancellationToken cancellationToken = default);
        
        Task<string> DownloadStringAsync(Uri url, CancellationToken cancellationToken = default);
        
        bool AllowMetaRedirect { get; set; }
        
        bool AutoDownloadPagesResources { get; set; }
        
        FakeUserAgent UserAgent { get; set; }
        
        Uri Referer { get; set; }
        
        void SetCookies(Uri cookieUrl, string cookiesExpression);
        
        Task<string> NavigateToAsync(Uri url, HttpVerb verb, string data, CancellationToken cancellationToken = default);

        Task<string> NavigateToAsync(Uri url, HttpVerb verb, NameValueCollection data, CancellationToken cancellationToken = default);

        Task<WebPage> NavigateToPageAsync(Uri url, HttpVerb verb = HttpVerb.Get, string data = "", string contentType = null, CancellationToken cancellationToken = default);
        
        Task<WebPage> NavigateToPageAsync(Uri url, HttpVerb verb, NameValueCollection data, CancellationToken cancellationToken = default);
        
        Cookie GetCookie(Uri url, string name);
        
        CookieCollection GetCookieCollection(Uri url);
    }
}