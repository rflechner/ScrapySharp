using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapySharp.Network
{
    public interface IScrapingBrowser : IDisposable
    {
        Task<WebResource> DownloadWebResourceAsync(Uri url, CancellationToken cancellationToken = default);
        
        Task<string> AjaxDownloadStringAsync(Uri url, CancellationToken cancellationToken = default);
        
        Task<string> DownloadStringAsync(Uri url, CancellationToken cancellationToken = default);
        
        bool AllowMetaRedirect { get; set; }
        
        bool AutoDownloadPagesResources { get; set; }
        
        UserAgent UserAgent { get; set; }
        
        Uri Referer { get; set; }
        
        void SetCookies(Uri cookieUrl, string cookiesExpression);
        
        Task<WebPage> NavigateToPageAsync(Uri url, HttpMethod verb = null, string data = "", string contentType = null, CancellationToken cancellationToken = default);
        
        Task<WebPage> NavigateToPageAsync(Uri url, HttpMethod verb, NameValueCollection data, CancellationToken cancellationToken = default);
        
        Cookie GetCookie(Uri url, string name);
        
        CookieCollection GetCookieCollection(Uri url);
    }
}