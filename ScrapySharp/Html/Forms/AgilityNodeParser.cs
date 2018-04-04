using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace ScrapySharp.Html.Forms
{
    internal class AgilityNodeParser : IHtmlNodeParser<HtmlNode>
    {
        private readonly HtmlNode node;

        public AgilityNodeParser(HtmlNode node)
        {
            this.node = node;

            Attributes = new NameValueCollection();

            foreach (var attribute in node.Attributes)
                Attributes.Add(attribute.Name, attribute.Value);
        }

        public IEnumerable<IHtmlNodeParser<HtmlNode>> CssSelect(string selector)
        {
            return node.CssSelect(selector).Select(n => new AgilityNodeParser(n));
        }

        public string GetAttributeValue(string name)
        {
            return node.GetAttributeValue(name, string.Empty);
        }

        public NameValueCollection Attributes { get; private set; }
        
        public string InnerText
        {
            get { return node.InnerText; }
        }
    }
}