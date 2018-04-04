using System.Collections.Generic;
using System.Collections.Specialized;

namespace ScrapySharp.Html.Forms
{
    internal interface IHtmlNodeParser<TNode>
    {
        IEnumerable<IHtmlNodeParser<TNode>> CssSelect(string selector);
        string GetAttributeValue(string name);
        NameValueCollection Attributes { get; }
        string InnerText { get; }
    }
}