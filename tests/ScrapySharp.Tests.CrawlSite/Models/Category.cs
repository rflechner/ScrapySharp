using System;
using ScrapySharp.Tests.CrawlSite.Models.ValueObjects;

namespace ScrapySharp.Tests.CrawlSite.Models
{
    public class Category
    {
        public CategoryId Id { get; }
        public string Name { get; }

        public Category(CategoryId id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}