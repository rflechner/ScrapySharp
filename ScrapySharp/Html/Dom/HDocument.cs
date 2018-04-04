using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScrapySharp.Html.Parsing;

namespace ScrapySharp.Html.Dom
{
    public class HDocument : HContainer
    {
        public HDocument(params HElement[] children)
        {
            Children = children.ToList();
        }

        public HDocument()
        {
            Children = new List<HElement>();
        }

        public static HDocument Parse(string source)
        {
            var codeReader = new CodeReader(source);
            var declarationReader = new HtmlDeclarationReader(codeReader);
            var domBuilder = new HtmlDomBuilder(declarationReader);

            return new HDocument
                       {
                           Children = domBuilder.BuildDom().ToList()
                       };
        }

        public string GetOuterHtml(HtmlGenerationStyle generationStyle = HtmlGenerationStyle.None)
        {
            var builder = new StringBuilder();

            var selfClosing = !HasChildren && !string.IsNullOrEmpty(innerText);

            if (!selfClosing)
            {
                if (!string.IsNullOrEmpty(innerText))
                    builder.Append(innerText);
                if (HasChildren)
                    foreach (var child in Children)
                        builder.Append(child.GetOuterHtml(generationStyle));
            }

            return builder.ToString();
        }
    }
}