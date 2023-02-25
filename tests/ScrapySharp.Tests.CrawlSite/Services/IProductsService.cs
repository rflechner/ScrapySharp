using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScrapySharp.Tests.CrawlSite.Models;
using ScrapySharp.Tests.CrawlSite.Models.ValueObjects;

namespace ScrapySharp.Tests.CrawlSite.Services
{
    public interface IProductsService
    {
        Task<Category> GetCategory(CategoryId id);
        
        IEnumerable<Category> GetCategories();
        
        PageItems<Product> GetProducts(CategoryId category, int page, int pageSize);
        Task<ProductCategoryView> GetProduct(ProductId id);
    }
}