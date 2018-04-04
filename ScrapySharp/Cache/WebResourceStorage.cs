using System;
using ScrapySharp.Network;
using System.Runtime.Caching;

namespace ScrapySharp.Cache
{
    public sealed class WebResourceStorage
    {
        private const string basePath = "_WebResourcesCache";
        private MemoryCache cache;

        public WebResourceStorage()
        {
            Initialize();
        }

        private void Initialize()
        {
            cache = new MemoryCache(basePath);
        }

        public void Save(WebResource webResource)
        {
            var cacheItem = new CacheItem(webResource.AbsoluteUrl.ToString(), webResource);
            var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddHours(2))
                };
            cache.AddOrGetExisting(cacheItem, policy);
        }

        public bool Exists(string key)
        {
            return cache.GetCacheItem(key) != null;
        }

        private static WebResourceStorage current;

        public static WebResourceStorage Current
        {
            get
            {
                if (current == null)
                    current = new WebResourceStorage();
                return current;
            }
        }
    }
}