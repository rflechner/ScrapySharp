using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;

namespace ScrapySharp.Html.Dom
{
    public class HElement : HContainer, IHSubContainer
    {
        public HElement(string name) : this()
        {
            Name = name;
        }

        public HElement(string name, string text, params IHSubContainer[] elements) : this(name, elements)
        {
            InnerText = text;
        }

        public HElement(string name, params IHSubContainer[] elements)
        {
            Name = name;
            Children = elements.OfType<HElement>().ToList();
            
            Attributes = new NameValueCollection();

            elements.OfType<HAttribute>().ToList()
                .ForEach(h => Attributes.Add(h.Name, h.Value));
        }

        public HElement()
        {
            Children = new List<HElement>();
            Attributes = new NameValueCollection();
        }

        public virtual string GetOuterHtml(HtmlGenerationStyle generationStyle = HtmlGenerationStyle.None)
        {
            var builder = new StringBuilder();

            var selfClosing = !HasChildren && string.IsNullOrEmpty(innerText);

            if (generationStyle == HtmlGenerationStyle.Indent)
                builder.Append(string.Empty.PadLeft(IndentLevel, '\t'));

            if (!string.IsNullOrEmpty(Name))
            {
                builder.Append('<');
                builder.Append(Name);
                
                if (HasAttributes)
                    foreach (var key in Attributes.AllKeys)
                        builder.AppendFormat(" {0}=\"{1}\"", key, Attributes[key]);

                if (!selfClosing)
                    builder.Append('>');
                else
                    builder.Append(" />");
            }

            if (!selfClosing)
            {
                if (HasChildren)
                {
                    if (generationStyle == HtmlGenerationStyle.Indent)
                        builder.AppendLine();
                    foreach (var child in Children)
                    {
                        child.IndentLevel = IndentLevel + 1;
                        builder.Append(child.GetOuterHtml(generationStyle));
                    }
                    
                    if (generationStyle == HtmlGenerationStyle.Indent)
                        builder.Append(string.Empty.PadLeft(IndentLevel, '\t'));
                }

                if (string.IsNullOrEmpty(innerText))
                    builder.AppendFormat("</{0}>", Name);

            }

            if (!string.IsNullOrEmpty(innerText))
            {
                builder.Append(innerText);
                if (!selfClosing && !string.IsNullOrEmpty(Name))
                    builder.AppendFormat("</{0}>", Name);
            }

            if (generationStyle == HtmlGenerationStyle.Indent)
                builder.AppendLine();

            return builder.ToString();
        }


        public string Id
        {
            get
            {
                if (HasAttributes)
                    return Attributes["id"];
                return string.Empty;
            }
            set
            {
                if (HasAttributes)
                    Attributes["id"] = value;
            }
        }

        public NameValueCollection Attributes { get; internal set; }

        public bool HasAttributes
        {
            get { return Attributes != null && Attributes.Count > 0; }
        }

        public HElement ParentNode { get; set; }
        
        public IEnumerable<HElement> Ancestors()
        {
            for (HElement node = this.ParentNode; node.ParentNode != null; node = node.ParentNode)
                yield return node.ParentNode;
        }

        public IEnumerable<HElement> Ancestors(string name)
        {
            for (HElement n = this.ParentNode; n != null; n = n.ParentNode)
            {
                if (n.Name == name)
                    yield return n;
            }
        }

        public string GetAttributeValue(string name, string def)
        {
            if (!HasAttributes)
                return def;
            var value = Attributes[name];
            if (value == null)
                return def;
            return value;
        }

        public int GetAttributeValue(string name, int def)
        {
            if (!HasAttributes)
                return def;
            var value = Attributes[name];
            if (value == null)
                return def;
            
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return def;
            }
        }

        public bool GetAttributeValue(string name, bool def)
        {
            if (!HasAttributes)
                return def;
            var value = Attributes[name];
            if (value == null)
                return def;
            
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return def;
            }
        }
    }

    public enum HtmlGenerationStyle
    {
        None,
        Indent
    }
}