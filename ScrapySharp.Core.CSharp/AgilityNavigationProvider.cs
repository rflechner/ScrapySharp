using HtmlAgilityPack;
using Microsoft.FSharp.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using static ScrapySharp.Core.CSharp.NavigationProvider;

namespace ScrapySharp.Core.CSharp
{
    public class AgilityNavigationProvider : INavigationProvider<HtmlNode>
    {
        public AgilityNavigationProvider()
        {
        }
        List<HtmlNode> INavigationProvider<HtmlNode>.ChildNodes(List<HtmlNode> nodes)
        {
            return new List<HtmlNode>(SeqModule.Collect<HtmlNodeCollection, HtmlNodeCollection, HtmlNode>(ChildNodes23._instance, SeqModule.Map<HtmlNode, HtmlNodeCollection>(ChildNodes231._instance, nodes)));
        }
        List<HtmlNode> INavigationProvider<HtmlNode>.Descendants(List<HtmlNode> nodes)
        {
            return new List<HtmlNode>(SeqModule.Collect<IEnumerable<HtmlNode>, IEnumerable<HtmlNode>, HtmlNode>(Descendants26._instance, SeqModule.Map<HtmlNode, IEnumerable<HtmlNode>>(Descendants261._instance, nodes)));
        }
        List<HtmlNode> INavigationProvider<HtmlNode>.ParentNodes(List<HtmlNode> nodes)
        {
            return new List<HtmlNode>(SeqModule.Map<HtmlNode, HtmlNode>(ParentNodes29._instance, nodes));
        }
        List<HtmlNode> INavigationProvider<HtmlNode>.AncestorsAndSelf(List<HtmlNode> nodes)
        {
            return new List<HtmlNode>(SeqModule.Collect<IEnumerable<HtmlNode>, IEnumerable<HtmlNode>, HtmlNode>(AncestorsAndSelf32._instance, SeqModule.Map<HtmlNode, IEnumerable<HtmlNode>>(AncestorsAndSelf321._instance, nodes)));
        }
        string INavigationProvider<HtmlNode>.GetName(HtmlNode node)
        {
            return node.Name;
        }
        string INavigationProvider<HtmlNode>.GetAttributeValue(HtmlNode node, string name, string defaultValue)
        {
            return node.GetAttributeValue(name, defaultValue);
        }
        string INavigationProvider<HtmlNode>.GetId(HtmlNode node)
        {
            return node.Id;
        }
        NameValueCollection INavigationProvider<HtmlNode>.Attributes(HtmlNode nodes)
        {
            NameValueCollection attrs = new NameValueCollection();
            HtmlAttributeCollection attributes = nodes.Attributes;
            foreach (HtmlAttribute attr in attributes)
            {
                attrs.Add(attr.Name, attr.Value);
            }
            return attrs;
        }
    }
}