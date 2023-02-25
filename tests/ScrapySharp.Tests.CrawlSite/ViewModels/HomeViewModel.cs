using ScrapySharp.Tests.CrawlSite.Models;

namespace ScrapySharp.Tests.CrawlSite.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel(Category[] categories)
        {
            Categories = categories;
        }

        public Category[] Categories { get; }
    }
}