using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace ScrapySharp.Extensions
{
    public static class HtmlCreationHelper
    {
        public static HtmlNode MergeInParentNode(this IEnumerable<HtmlNode> nodes, string name)
        {
            var doc = new HtmlDocument();
            var htmlNode = doc.CreateElement(name);
            nodes.ToList().ForEach(n => htmlNode.AppendChild(n));

            return htmlNode;
        }
    }
}