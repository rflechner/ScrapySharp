using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using ScrapySharp.Exceptions;
using ScrapySharp.IntegrationTests.Core;
using ScrapySharp.Network;
using ScrapySharp.Tests.CrawlSite;
using Xunit;

namespace ScrapySharp.IntegrationTests
{
    public class ModernScrapingBrowser_DownloadStringAsyncTests : WebApiTests
    {
        public ModernScrapingBrowser_DownloadStringAsyncTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }
        
        [Fact]
        public async Task HomePage_ShouldGetTextContent()
        {
            var browser = CreateModernScrapingBrowser();

            var content = await browser.DownloadStringAsync(CreateUri("/"));
            
            Assert.NotEmpty(content);

            Assert.Contains("</html>", content);
        }

        [Fact]
        public async Task NotExistingPage_ShouldThrow()
        {
            var browser = CreateModernScrapingBrowser();

            await Assert.ThrowsAsync<ScrapingException>(async () =>
            {
                await browser.DownloadStringAsync(CreateUri("/lalala/popopo"));
            });
        }

        

    }
}