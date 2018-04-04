using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;

namespace ScrapySharp.Html.Dom
{
    public abstract class HContainer
    {
        private string name;
        protected string innerText;


        protected HContainer()
        {
            Children = new List<HElement>();
        }

        public List<HElement> Children { get; set; }
        
        public bool HasChildren
        {
            get { return Children != null && Children.Any(); }
        }

        public int IndentLevel { get; set; }

        public string Name
        {
            get
            {
                if (name == null)
                    return string.Empty;
                return name;
            }
            set { name = value; }
        }

        public string InnerText
        {
            get
            {
                if (innerText == null)
                    innerText = string.Empty;

                var builder = new StringBuilder();
                builder.Append(innerText);

                if (Children != null)
                    foreach (var child in Children)
                        builder.Append(child.InnerText);

                return WebUtility.HtmlDecode(builder.ToString());
            }
            set { innerText = value; }
        }

        public string InnerHtml
        {
            get
            {
                var builder = new StringBuilder();
                foreach (var child in Children)
                {
                    builder.Append(child.GetOuterHtml());
                }

                return builder.ToString();
            }
            set
            {
                Children.Clear();
                Children.AddRange(HDocument.Parse(value).Children);
            }
        }
    }
}