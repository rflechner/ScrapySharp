using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ScrapySharp.Network;
using ScrapySharp.Tests.CrawlSite;
using Xunit;

namespace ScrapySharp.IntegrationTests.Core
{
    public abstract class WebApiTests: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected readonly WebApplicationFactory<Startup> Factory;
        protected readonly CustomWebApplicationFactory<Startup> CustomFactory;

        protected WebApiTests(CustomWebApplicationFactory<Startup> factory)
        {
            CustomFactory = factory;
            Factory = factory.WithWebHostBuilder(ConfigureWebHostBuilder);
            Factory.ClientOptions.BaseAddress = BaseAddress;
        }
        
        protected Uri BaseAddress => new Uri("http://localhost/");

        protected Uri CreateUri(string path)
        {
            var builder = new UriBuilder(BaseAddress)
            {
                Path = path
            };

            return builder.Uri;
        }

        protected ModernScrapingBrowser CreateModernScrapingBrowser() => new ModernScrapingBrowser(Factory.CreateClient(), "MyBot");

        protected virtual void ConfigureWebHostBuilder(IWebHostBuilder builder)
        {
            
        }

        public async Task<HttpResponseMessage> PostJson<T>(HttpClient client, string path, T model)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model));
            content.Headers.ContentType.MediaType = "application/json";
            return await client.PostAsync(path, content);
        }
    }
}