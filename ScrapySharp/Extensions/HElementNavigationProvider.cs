using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ScrapySharp.Core;
using ScrapySharp.Html.Dom;

namespace ScrapySharp.Extensions
{
    public class HElementNavigationProvider : INavigationProvider<HElement>
    {
        public List<HElement> ChildNodes(List<HElement> nodes)
        {
            return nodes.SelectMany(n => n.Children).ToList();
        }

        public List<HElement> Descendants(List<HElement> nodes)
        {
            return nodes.SelectMany(n => n.Descendants()).ToList();
        }

        public List<HElement> ParentNodes(List<HElement> nodes)
        {
            return nodes.Select(n => n.ParentNode).ToList();
        }

        public List<HElement> AncestorsAndSelf(List<HElement> nodes)
        {
            return nodes.SelectMany(n => n.Ancestors()).Concat(nodes).ToList();
        }

        public string GetName(HElement node)
        {
            return node.Name;
        }

        public string GetAttributeValue(HElement node, string name, string defaultValue)
        {
            return node.GetAttributeValue(name, defaultValue);
        }

        public string GetId(HElement node)
        {
            return node.Id;
        }

        public NameValueCollection Attributes(HElement node)
        {
            if (node.Attributes == null)
                return new NameValueCollection();
            return node.Attributes;
        }
    }
}