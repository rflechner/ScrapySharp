using HtmlAgilityPack;
using Microsoft.FSharp.Core;
using System.Collections.Generic;
namespace ScrapySharp.Core.CSharp
{
    internal static class NavigationProvider
    {
        internal sealed class ChildNodes23 : FSharpFunc<HtmlNodeCollection, HtmlNodeCollection>
        {
            public override HtmlNodeCollection Invoke(HtmlNodeCollection x)
            {
                return x;
            }
            internal static readonly ChildNodes23 _instance = new ChildNodes23();
        }
        internal sealed class ChildNodes231 : FSharpFunc<HtmlNode, HtmlNodeCollection>
        {
            public override HtmlNodeCollection Invoke(HtmlNode x)
            {
                return x.ChildNodes;
            }
            internal static readonly ChildNodes231 _instance = new ChildNodes231();
        }
        internal sealed class Descendants26 : FSharpFunc<IEnumerable<HtmlNode>, IEnumerable<HtmlNode>>
        {
            public override IEnumerable<HtmlNode> Invoke(IEnumerable<HtmlNode> x)
            {
                return x;
            }
            internal static readonly Descendants26 _instance = new Descendants26();
        }
        internal sealed class Descendants261 : FSharpFunc<HtmlNode, IEnumerable<HtmlNode>>
        {
            public override IEnumerable<HtmlNode> Invoke(HtmlNode x)
            {
                return x.Descendants();
            }
            internal static readonly Descendants261 _instance = new Descendants261();
        }
        internal sealed class ParentNodes29 : FSharpFunc<HtmlNode, HtmlNode>
        {
            public override HtmlNode Invoke(HtmlNode x)
            {
                return x.ParentNode;
            }
            internal static readonly ParentNodes29 _instance = new ParentNodes29();
        }
        internal sealed class AncestorsAndSelf32 : FSharpFunc<IEnumerable<HtmlNode>, IEnumerable<HtmlNode>>
        {
            public override IEnumerable<HtmlNode> Invoke(IEnumerable<HtmlNode> x)
            {
                return x;
            }
            internal static readonly AncestorsAndSelf32 _instance = new AncestorsAndSelf32();
        }
        internal sealed class AncestorsAndSelf321 : FSharpFunc<HtmlNode, IEnumerable<HtmlNode>>
        {
            public override IEnumerable<HtmlNode> Invoke(HtmlNode x)
            {
                return x.AncestorsAndSelf();
            }
            internal static readonly AncestorsAndSelf321 _instance = new AncestorsAndSelf321();
		}
    }
}
