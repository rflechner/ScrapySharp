using System;
using System.Text.RegularExpressions;

namespace ScrapySharp.Extensions
{
    public static class UrlHelper
    {
        private static readonly Regex BasePathRegex = new ("(?<scheme>(http[s]?[:]//)?)(?<site>[^/]+).*", RegexOptions.Compiled);

        public static Uri Combine(this Uri uri, string path)
        {
            var url = uri.ToString();
            return CombineUrl(url, path);
        }

        public static Uri CombineUrl(this string url, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return new Uri(url);

            if (path.StartsWith("/"))
            {
                var match = BasePathRegex.Match(url);
                if (match.Success)
                {
                    var scheme = match.Groups["scheme"].Value;
                    var site = match.Groups["site"].Value;

                    return new Uri(scheme + site + path);
                }
            }

            if (!url.EndsWith("/"))
                url += '/';

            string combined;
            if (url.EndsWith("/") && path.StartsWith("/"))
                combined = url + path.Substring(1);
            else
                combined = url + path;

            return new Uri(combined);
        }
    }
}