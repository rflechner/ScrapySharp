using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScrapySharp.IntegrationTests.Core;
using ScrapySharp.Network;
using ScrapySharp.Tests.CrawlSite;
using Xunit;

namespace ScrapySharp.IntegrationTests
{
    public class ModernScrapingBrowser_FakeUserAgentTests : WebApiTests
    {
        public ModernScrapingBrowser_FakeUserAgentTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task DetectBrowser_ShouldNotReceiveUserAgent()
        {
            var browser = CreateModernScrapingBrowser();
            browser.UserAgent = null;

            WebPage page = await browser.NavigateToPageAsync(CreateUri("/Home/DetectBrowser"));
            
            Assert.True(page.IsErrorPage);
            
            Assert.Equal("Missing user agent header", page.Content);
        }

        [Theory]
        [ClassData(typeof(FakeUserAgentTestData))]
        public async Task DetectBrowser_ShouldDetectBrowser(string browserName, string device, UserAgent userAgent)
        {
            var browser = CreateModernScrapingBrowser();
            browser.UserAgent = userAgent;
            
            var content = await browser.DownloadStringAsync(CreateUri("/Home/DetectBrowser"));
            
            Assert.Equal($"{browserName} for {device}", content);
        }

        private class FakeUserAgentTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "Google Chrome","Windows", FakeUserAgents.ChromeForWindows };
                yield return new object[] { "Mozilla Firefox","Windows", new UserAgent("Firefox", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:75.0) Gecko/20100101 Firefox/75.0") };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}