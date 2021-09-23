using Microsoft.FSharp.Core;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using HtmlAgilityPack;

namespace ScrapySharp.Core.CSharp
{
    public interface INavigationProvider<t>
    {
		List<t> ChildNodes(List<t> ts);

		List<t> Descendants(List<t> ts);
        List<t> ParentNodes(List<t> ts);

		List<t> AncestorsAndSelf(List<t> ts);

		string GetName(t ts);

		[CompilationArgumentCounts(new int[]
		{
			1,
			1,
			1
		})]
		string GetAttributeValue(t ts, string n1, string n2);

		string GetId(t ts);

		NameValueCollection Attributes(t ts);
	}
}