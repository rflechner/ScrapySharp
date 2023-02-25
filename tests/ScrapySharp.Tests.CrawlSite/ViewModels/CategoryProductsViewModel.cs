using System.Collections.Immutable;
using ScrapySharp.Tests.CrawlSite.Models;

namespace ScrapySharp.Tests.CrawlSite.ViewModels
{
    public class CategoryProductsViewModel
    {
        public CategoryProductsViewModel(Category category, ImmutableArray<Product> products, int page, int pageSize, int pageCount)
        {
            Category = category;
            Page = page;
            PageSize = pageSize;
            PageCount = pageCount;
            Products = products;
        }

        public Category Category { get; }
        
        public int Page { get; }
        
        public int PageSize { get; }
        public int PageCount { get; }

        public ImmutableArray<Product> Products { get; }

        public bool IsFirstPage => Page <= 1;

        public bool IsLastPage => Page >= PageCount;
    }
}