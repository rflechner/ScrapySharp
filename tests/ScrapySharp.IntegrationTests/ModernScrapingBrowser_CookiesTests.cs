using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using ScrapySharp.IntegrationTests.Core;
using ScrapySharp.Network;
using ScrapySharp.Tests.CrawlSite;
using Xunit;

namespace ScrapySharp.IntegrationTests
{
    public class ModernScrapingBrowser_CookiesTests : WebApiTests
    {
        public ModernScrapingBrowser_CookiesTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        [InlineData(5)]
        [InlineData(59)]
        public async Task GetCount_ShouldReturnIncrementedValue(int iterations)
        {
            var browser = CreateModernScrapingBrowser();
            
            for (var i = 1; i <= iterations; i++)
            {
                var page = await browser.NavigateToPageAsync(CreateUri("/CookiesTest/IncCount"));
                var cookieHeader = page.RawResponse.Headers.First(h => h.Name == "Set-Cookie");
                var cookiesParser = new CookiesParser("localhost");
                var cookies = cookiesParser.ParseCookies(cookieHeader.Values.Single()).ToArray();
                var cookie = cookies.Single(c => c.Name == "VisitsCount");
                
                Assert.Equal(i.ToString(), cookie.Value);
                
                var content = await browser.DownloadStringAsync(CreateUri("/CookiesTest/GetCount"));
                var count = int.Parse(content.Trim());
                
                Assert.Equal(i, count);
            }
            
            var finalCount = int.Parse((await browser.DownloadStringAsync(CreateUri("/CookiesTest/GetCount"))).Trim());
                
            Assert.Equal(iterations, finalCount);
        }

        [Fact]
        public async Task GetCount_ShouldNotIncrementWhenClientHandlerDoesntHandleCookies()
        {
            var httpClient = Factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                HandleCookies = false
            });

            var browser = new ModernScrapingBrowser(httpClient, "MyBot");

            for (var i = 1; i <= 2; i++)
            {
                await browser.DownloadStringAsync(CreateUri("/CookiesTest/IncCount"));
                var content = await browser.DownloadStringAsync(CreateUri("/CookiesTest/GetCount"));
                var count = int.Parse(content.Trim());
                
                Assert.Equal(0, count);
            }
            
            var finalCount = int.Parse((await browser.DownloadStringAsync(CreateUri("/CookiesTest/GetCount"))).Trim());
                
            Assert.Equal(0, finalCount);
        }
    }
}