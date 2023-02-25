using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using ScrapySharp.Tests.CrawlSite.Models;
using ScrapySharp.Tests.CrawlSite.Models.ValueObjects;

namespace ScrapySharp.Tests.CrawlSite.Services
{
    public sealed class ProductsService : IProductsService
    {
        private readonly Category[] categories;
        private readonly ConcurrentDictionary<ProductId, ProductCategoryView> products;
        private readonly ConcurrentDictionary<CategoryId, ProductId[]> productsByCategories;

        public ProductsService()
        {
            categories = new Faker().Commerce.Categories(40).Distinct()
                .Select(name => new Category(CategoryId.NewId(), name))
                .ToArray();
            
            productsByCategories = new ConcurrentDictionary<CategoryId, ProductId[]>();
            products = new ConcurrentDictionary<ProductId, ProductCategoryView>();

            GenerateEntities();
        }

        private void GenerateEntities()
        {
            foreach (var category in categories)
            {
                var generatedProducts = GenerateProducts();
                foreach (var product in generatedProducts) products.TryAdd(product.Id, new ProductCategoryView(category, product));
                
                productsByCategories.TryAdd(category.Id, generatedProducts.Select(p => p.Id).ToArray());
            }
        }

        private Product[] GenerateProducts(int count = 300)
        {
            var faker = new Faker<Product>()
                .CustomInstantiator(f => new Product(ProductId.NewId(), f.Commerce.ProductName(), f.Random.Int(0, 50_000)));

            return faker.Generate(count).ToArray();
        }

        public IEnumerable<Category> GetCategories() => categories;

        public Task<Category> GetCategory(CategoryId id) => Task.FromResult(categories.SingleOrDefault(c => c.Id.Equals(id)));

        public Task<ProductCategoryView> GetProduct(ProductId id) => Task.FromResult(products[id]);

        public PageItems<Product> GetProducts(CategoryId categoryId, int page, int pageSize)
        {
            if (pageSize <= 0)
                pageSize = 20;
            
            if (page <= 0)
                page = 1;

            var productIds = productsByCategories[categoryId];
            var pageCount = productIds.Length/pageSize;
            var offset = page - 1;

            return new PageItems<Product>(page, pageCount, productIds.Select(id => products[id].Product).Skip(offset*pageSize).Take(pageSize).ToImmutableArray(), pageSize);
        }
    }
}