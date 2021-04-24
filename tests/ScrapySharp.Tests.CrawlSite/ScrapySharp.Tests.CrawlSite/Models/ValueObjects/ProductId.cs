using System;

namespace ScrapySharp.Tests.CrawlSite.Models.ValueObjects
{
    public readonly struct ProductId
    {
        public ProductId(Guid value) => Value = value;

        public Guid Value { get; }

        public bool Equals(ProductId other) => Value.Equals(other.Value);

        public override bool Equals(object obj) => obj is ProductId other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString("N");
        
        public static ProductId Parse(string text) => new ProductId(Guid.Parse(text));
        
        public static readonly ProductId Empty = new ProductId(Guid.Empty);
        
        public static ProductId NewId() => new ProductId(Guid.NewGuid());
    }
}