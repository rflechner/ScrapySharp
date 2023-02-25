using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ScrapySharp.Core;
using ScrapySharp.Html.Dom;

namespace ScrapySharp.Extensions
{
    public class HElementNavigationProvider : INavigationProvider<HElement>
    {
        public IEnumerable<HElement> ChildNodes(IEnumerable<HElement> nodes) => nodes.SelectMany(n => n.Children);

        public IEnumerable<HElement> Descendants(IEnumerable<HElement> nodes) => nodes.SelectMany(n => n.Descendants());

        public IEnumerable<HElement> ParentNodes(IEnumerable<HElement> nodes) => nodes.Select(n => n.ParentNode);

        public IEnumerable<HElement> AncestorsAndSelf(IEnumerable<HElement> nodes)
        {
            foreach (var n in nodes)
            {
                foreach (var ancestor in n.Ancestors())
                {
                    yield return ancestor;
                }

                yield return n;
            }
        }

        public string GetName(HElement node) => node.Name;

        public string GetAttributeValue(HElement node, string name, string defaultValue) => node.GetAttributeValue(name, defaultValue);

        public string GetId(HElement node) => node.Id;

        public NameValueCollection Attributes(HElement node) => node.Attributes ?? new NameValueCollection();
    }
}