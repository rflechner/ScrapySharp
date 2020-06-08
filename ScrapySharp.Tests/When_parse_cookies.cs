// ReSharper disable InconsistentNaming

using System.IO;
using NUnit.Framework;
using ScrapySharp.Html.Dom;
using ScrapySharp.Html.Parsing;
using System.Linq;
using ScrapySharp.Network;
using System.Net;

namespace ScrapySharp.Tests
{
    [TestFixture]
    public class When_parse_cookies
    {
        [Test]
        public void When_parse_standard_cookie()
        {
            var cookie = GetCookie();

            CookiesParser parser = new CookiesParser(".localhost.fakedomain");
            var cookieList = parser.ParseCookies(cookie);

            Assert.AreEqual(2, cookieList.Count);
        }

        [Test]
        public void When_parse_csv_cookie()
        {
            var csvCookie = GetCookie().Replace(";",",");            

            CookiesParser parser = new CookiesParser(".localhost.fakedomain");
            var cookieList = parser.ParseCookies(csvCookie);

            Assert.AreEqual(2, cookieList.Count);
        }

        [Test]
        public void When_parse_csv_invalid_cookie()
        {
            string invalidCookie = GetCookie().Replace(";",";,");
            CookiesParser parser = new CookiesParser(".localhost.fakedomain");
            
            Assert.Throws<CookieException>(()=> { parser.ParseCookies(invalidCookie); });
        }

        private static string GetCookie()
        {
            var cookie = File.ReadAllText("Network/Cookie.txt");

            return cookie;
        }
    }
}

// ReSharper restore InconsistentNaming