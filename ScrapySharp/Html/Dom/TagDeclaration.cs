using System.Collections.Generic;
using System.Collections.Specialized;
using ScrapySharp.Html.Parsing;

namespace ScrapySharp.Html.Dom
{
    public class TagDeclaration
    {
        public string InnerText { get; set; }

        public string Name { get; set; }

        public NameValueCollection Attributes { get; set; }

        public List<Word> Words { get; set; }

        public DeclarationType Type { get; set; }
    }
}