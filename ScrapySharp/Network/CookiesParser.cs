using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace ScrapySharp.Network
{
    public class CookiesParser
    {
        private readonly string defaultDomain;
        private static readonly Regex splitCookiesRegex = new Regex(@"\s*(?<name>[^=]+)=(?<val>[^;]+)?[,;]+", RegexOptions.Compiled);

        public CookiesParser(string defaultDomain)
        {
            this.defaultDomain = defaultDomain;
        }

        public List<KeyValuePair<string, string>> ParseValuePairs(string cookiesExpression)
        {
            var list = new List<KeyValuePair<string, string>>();

            var match = splitCookiesRegex.Match(cookiesExpression);

            while (match.Success)
            {
                if (match.Groups["name"].Success && match.Groups["val"].Success)
                {
                    try
                    {
                        list.Add(new KeyValuePair<string, string>(match.Groups["name"].Value, match.Groups["val"].Value));
                    }
                    catch (CookieException) { }
                }
                match = match.NextMatch();
            }

            return list;
        }

        public List<Cookie> ParseCookies(string cookiesExpression)
        {
            var cookies = new List<Cookie>();
            var keyValuePairs = ParseValuePairs(cookiesExpression);

            for (int i = 0; i < keyValuePairs.Count; i++)
            {
                var pair = keyValuePairs[i];
                if (pair.Key.Equals("path", StringComparison.InvariantCultureIgnoreCase) 
                    || pair.Key.Equals("domain", StringComparison.InvariantCultureIgnoreCase)
                    || pair.Key.Equals("expires", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                var name = pair.Key;
                var value = pair.Value;
                string path = null;
                string domain = null;

                var next1 = i + 1;
                if (next1 < keyValuePairs.Count)
                {
                    if (keyValuePairs[next1].Key.Equals("path", StringComparison.InvariantCultureIgnoreCase))
                        path = keyValuePairs[next1].Value;
                    if (keyValuePairs[next1].Key.Equals("domain", StringComparison.InvariantCultureIgnoreCase))
                        domain = keyValuePairs[next1].Value;
                }

                var next2 = i + 2;
                if (next2 < keyValuePairs.Count)
                {
                    if (keyValuePairs[next2].Key.Equals("path", StringComparison.InvariantCultureIgnoreCase))
                        path = keyValuePairs[next2].Value;
                    if (keyValuePairs[next2].Key.Equals("domain", StringComparison.InvariantCultureIgnoreCase))
                        domain = keyValuePairs[next2].Value;
                }

                if (string.IsNullOrEmpty(domain) && !string.IsNullOrEmpty(path))
                    cookies.Add(new Cookie(name, value, path, defaultDomain));
                else if (!string.IsNullOrEmpty(domain) && !string.IsNullOrEmpty(path))
                    cookies.Add(new Cookie(name, value, path, domain));
                else
                    cookies.Add(new Cookie(name, value, "/", defaultDomain));
            }

            return cookies;
        }
    }
}