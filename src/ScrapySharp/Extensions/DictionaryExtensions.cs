using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using HtmlAgilityPack;

namespace ScrapySharp.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool HasKeyIgnoreCase(this HtmlAttributeCollection dictionary, string name)
        {
            if (dictionary == null)
                return false;

            var key = dictionary.FirstOrDefault(k => k.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (key == null)
                return false;

            return true;
        }

        public static string GetIgnoreCase(this HtmlAttributeCollection dictionary, string name)
        {
            if (dictionary == null)
                return null;

            var key = dictionary.FirstOrDefault(k => k.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (key == null)
                return null;

            return dictionary[key.Name].Value;
        }

        public static bool HasKeyIgnoreCase(this NameValueCollection dictionary, string name)
        {
            if (dictionary == null)
                return false;

            var key = dictionary.AllKeys.FirstOrDefault(k => k.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (key == null)
                return false;

            return true;
        }

        public static string GetIgnoreCase(this NameValueCollection dictionary, string name)
        {
            if (dictionary == null)
                return null;

            var key = dictionary.AllKeys.FirstOrDefault(k => k.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (key == null)
                return null;

            return dictionary[key];
        }

        public static string GetIgnoreCase(this IDictionary<string, string> dictionary, string name)
        {
            if (dictionary == null)
                return null;

            var key = dictionary.Keys.FirstOrDefault(k => k.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (key == null)
                return null;

            return dictionary[key];
        }
    }
}