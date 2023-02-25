using System.Collections.Immutable;

namespace ScrapySharp.Tests.CrawlSite.Models
{
    public class PageItems<T>
    {
        public int Page { get; }
        
        public int PageCount { get; }
        
        public ImmutableArray<T> Items { get; }
        
        public int PageSize { get; }

        public PageItems(int page, int pageCount, ImmutableArray<T> items, int pageSize)
        {
            Page = page;
            PageCount = pageCount;
            Items = items;
            PageSize = pageSize;
        }
    }
}