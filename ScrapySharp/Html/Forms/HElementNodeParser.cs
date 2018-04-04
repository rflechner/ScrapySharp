using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ScrapySharp.Extensions;
using ScrapySharp.Html.Dom;

namespace ScrapySharp.Html.Forms
{
    internal class HElementNodeParser : IHtmlNodeParser<HElement>
    {
        private readonly HElement node;

        public HElementNodeParser(HElement node)
        {
            this.node = node;
            Attributes = node.Attributes;
        }

        public IEnumerable<IHtmlNodeParser<HElement>> CssSelect(string selector)
        {
            return node.CssSelect(selector).Select(n => new HElementNodeParser(n));
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