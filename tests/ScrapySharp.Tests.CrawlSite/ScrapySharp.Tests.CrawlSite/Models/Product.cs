using System;
using ScrapySharp.Tests.CrawlSite.Models.ValueObjects;

namespace ScrapySharp.Tests.CrawlSite.Models
{
    public class Product
    {
        public Product(ProductId id, string name, int likesCount)
        {
            Id = id;
            Name = name;
            LikesCount = likesCount;
        }
        
        public ProductId Id { get; }
        
        public string Name { get; }
        
        public int LikesCount { get; }

        private bool Equals(Product other)
        {
            return Id.Equals(other.Id) && Name == other.Name && LikesCount == other.LikesCount;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Product) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, LikesCount);
        }
    }
}