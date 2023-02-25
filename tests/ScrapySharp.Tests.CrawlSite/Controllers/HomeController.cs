using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScrapySharp.Tests.CrawlSite.Helpers;
using ScrapySharp.Tests.CrawlSite.Models;
using ScrapySharp.Tests.CrawlSite.Models.ValueObjects;
using ScrapySharp.Tests.CrawlSite.Services;
using ScrapySharp.Tests.CrawlSite.ViewModels;

namespace ScrapySharp.Tests.CrawlSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IProductsService productsService;

        public HomeController(ILogger<HomeController> logger, IProductsService productsService)
        {
            this.logger = logger;
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            return View(new HomeViewModel(productsService.GetCategories().ToArray()));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public async Task<IActionResult> Category(Guid id, int page=0, int pageSize=20)
        {
            var categoryId = new CategoryId(id);
            var category = await productsService.GetCategory(categoryId);
            var products = productsService.GetProducts(categoryId, page, pageSize);

            return View(new CategoryProductsViewModel(category, products.Items, products.Page, products.PageSize, products.PageCount));
        }

        public async Task<IActionResult> Product(Guid id)
        {
            var product = await productsService.GetProduct(new ProductId(id));

            return View(product);
        }
        
        public IActionResult DetectBrowser()
        {
            if (!Request.Headers.TryGetValue("User-Agent", out var userAgents))
                return StatusCode((int)HttpStatusCode.BadRequest, "Missing user agent header");

            var userAgent = userAgents.ToString();
            if (userAgent == null)
                return StatusCode((int)HttpStatusCode.BadRequest, "Invalid user agent header");

            var browserName = UserAgentHelper.GetBrowserName(userAgent);
            if (string.IsNullOrEmpty(browserName))
                return Content("I don't know your browser");

            var device = UserAgentHelper.GetOsDevice(userAgent);
            
            if (string.IsNullOrWhiteSpace(device))
                return Content(browserName);

            return Content($"{browserName} for {device}");
        }

    }
}