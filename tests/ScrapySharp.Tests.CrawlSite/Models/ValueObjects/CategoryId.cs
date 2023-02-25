using System;

namespace ScrapySharp.Tests.CrawlSite.Models.ValueObjects
{
    public readonly struct CategoryId
    {
        public CategoryId(Guid value) => Value = value;

        public Guid Value { get; }

        public bool Equals(CategoryId other) => Value.Equals(other.Value);

        public override bool Equals(object obj) => obj is CategoryId other && Equals(other);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString("N");
        
        public static CategoryId Parse(string text) => new CategoryId(Guid.Parse(text));
        
        public static readonly CategoryId Empty = new CategoryId(Guid.Empty);
        
        public static CategoryId NewId() => new CategoryId(Guid.NewGuid());
    }
}