using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.IntegrationTests.Core;
using ScrapySharp.IntegrationTests.ScrapingServicesSamples;
using ScrapySharp.Tests.CrawlSite;
using ScrapySharp.Tests.CrawlSite.Models;
using ScrapySharp.Tests.CrawlSite.Models.ValueObjects;
using Xunit;

namespace ScrapySharp.IntegrationTests
{
    public class ModernScrapingBrowser_NavigationTests : WebApiTests
    {
        public ModernScrapingBrowser_NavigationTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }
        
        [Fact]
        public async Task NavigateToPageAsync_ShouldCrawlCategoriesList()
        {
            var scrapingService = new ProductsScrapingService(CreateModernScrapingBrowser(), BaseAddress);
            var categories = await scrapingService.GetCategoriesLinks().Select(c => c.name).ToArrayAsync();

            string[] expectedCategories = CustomFactory.FakeProductsService.GetCategories().Select(c => c.Name).ToArray();
            
            Assert.True(expectedCategories.SequenceEqual(categories));
        }
        
        [Fact]
        public async Task NavigateToPageAsync_ShouldCrawlProductsByCategory()
        {
            var browser = CreateModernScrapingBrowser();
            var scrapingService = new ProductsScrapingService(browser, BaseAddress);
            var categories = await scrapingService.GetCategoriesLinks().ToArrayAsync();
            
            var expectedCategories = this.CustomFactory.FakeProductsService.GetCategories().Select(c => c.Name);
            Assert.True(expectedCategories.SequenceEqual(categories.Select(c => c.name)));

            foreach (var category in categories)
            {
                var categoryProducts = await scrapingService.GetCategoryProducts(category.link).ToArrayAsync();
                var id = Guid.Parse(category.link.Replace("/Home/Category/", string.Empty));
                var expectedProducts = CustomFactory.FakeProductsService.GetProducts(new CategoryId(id), 0, int.MaxValue).Items;
                
                Assert.True(categoryProducts.SequenceEqual(expectedProducts));
            }
        }
    }
}