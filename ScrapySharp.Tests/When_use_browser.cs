using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;
using ScrapySharp.Network;
using ScrapySharp.Extensions;

namespace ScrapySharp.Tests
{
    [TestFixture]
    public class When_use_browser
    {
        [Test, Ignore("legacy")]
        [Category("Integration")]
        public void When_using_raw_request_response()
        {
            var browser = new ScrapingBrowser();
            //browser.Encoding = Encoding.UTF8;
            WebPage page = browser.NavigateToPage(new Uri("https://bitbucket.org/repo/all"), HttpVerb.Post, new NameValueCollection
                {
                    {"name", "test"},
                });

            page.SaveSnapshot("outdir");
        }

        [Test]
        public void When_combine_urls()
        {
            var baseUrl = "http://toto.dada.com/izi/";
            var relative1 = "../general/images/izi/logo.gif";
            var abs1 = baseUrl.CombineUrl(relative1).ToString();

            Assert.AreEqual("http://toto.dada.com/general/images/izi/logo.gif", abs1);


            var relative2 = "/images/izi/logo.gif";
            var abs2 = baseUrl.CombineUrl(relative2).ToString();

            Assert.AreEqual("http://toto.dada.com/images/izi/logo.gif", abs2);

        }

        [Test]
        [Category("Integration")]
        public void When_parses_cookies()
        {
            var exp1 = @"FBXSID=""8KgAN7h4ZQsvn9OWXy1fvBlrNuRdIr4J0bkguqR5AIdL7clHgA+NQ5URtThL10od""; Max-Age=86400; HTTPOnly";
            var cookieContainer = new CookieContainer();
            cookieContainer.SetCookies(new Uri("http://www.popo.com"), exp1);

            Assert.AreEqual(1, cookieContainer.Count);
        }

        [Test, Ignore("Integration")]
        [Category("Integration")]
        public void When_forcing_anguage()
        {
            var browser1 = new ScrapingBrowser();
            var html1 = browser1.DownloadString(new Uri("http://www.google.com"));

            var browser2 = new ScrapingBrowser {Language = CultureInfo.CreateSpecificCulture("fr-FR")};
            var html2 = browser2.DownloadString(new Uri("http://www.google.com"));

            Assert.AreNotEqual(html1, html2);
        }
        
    }
}