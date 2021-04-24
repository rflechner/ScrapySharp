namespace ScrapySharp.Tests.CrawlSite.Models
{
    public class ProductCategoryView
    {
        public Category Category { get; }
        public Product Product { get; }

        public ProductCategoryView(Category category, Product product)
        {
            Category = category;
            Product = product;
        }
    }
}