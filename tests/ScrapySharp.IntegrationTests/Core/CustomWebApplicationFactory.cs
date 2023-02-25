using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ScrapySharp.Tests.CrawlSite.Services;

namespace ScrapySharp.IntegrationTests.Core
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public readonly ProductsService FakeProductsService = new ();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Add(new ServiceDescriptor(typeof(IProductsService), FakeProductsService));
            });
        }
    }
}