using System;
using System.Collections.Generic;

namespace ScrapySharp.Tests.CrawlSite.Helpers
{
    public static class UserAgentHelper
    {
        public static string GetOsDevice(string userAgent)
        {
            var names = new []
            {
                "Macintosh", "Windows", "Linux", "iPhone", "iPad", "iPod", "Android"
            };

            foreach (var name in names)
            {
                if (userAgent.Contains(name, StringComparison.OrdinalIgnoreCase))
                    return name;
            }

            return string.Empty;
        }
            
        public static string GetBrowserName(string userAgent)
        {
            var names = new Dictionary<string, string>
            {
                {"Chrome", "Google Chrome"},
                {"Firefox", "Mozilla Firefox"},
                {"Opera", "Opera"},
                {"Safari", "Safari"},
                {"MSIE", "Internet Explorer"},
            };

            foreach (var pattern in names.Keys)
            {
                if (userAgent.Contains(pattern, StringComparison.OrdinalIgnoreCase))
                    return names[pattern];
            }

            return string.Empty;
        }
    }
}