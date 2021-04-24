using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace ScrapySharp.Html
{
    public class ElementFinder
    {
        private readonly HtmlNode html;
        private readonly ElementSearchKind searchKind;
        private readonly string tagName;
        private readonly string query;
        private readonly StringComparison comparisonType;

        internal ElementFinder(HtmlNode html, ElementSearchKind searchKind, string tagName, string query, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            this.html = html;
            this.searchKind = searchKind;
            this.tagName = tagName;
            this.query = query;
            this.comparisonType = comparisonType;
        }

        public IEnumerable<HtmlNode> FindElements() =>
            searchKind switch
            {
                ElementSearchKind.Text => html.Descendants(tagName)
                    .Where(n => string.IsNullOrEmpty(n.InnerText)
                        ? string.IsNullOrEmpty(query)
                        : n.InnerText.Equals(query, comparisonType)),
                ElementSearchKind.Id => from n in html.Descendants(tagName)
                    where string.IsNullOrEmpty(n.Id) ? string.IsNullOrEmpty(query) : n.Id.Equals(query, comparisonType)
                    select n,
                ElementSearchKind.Name => from n in html.Descendants(tagName)
                    let name = n.GetAttributeValue("name", string.Empty)
                    where string.IsNullOrEmpty(name) ? string.IsNullOrEmpty(query) : name.Equals(query, comparisonType)
                    select n,
                ElementSearchKind.Class => from n in html.Descendants(tagName)
                    let @class = n.GetAttributeValue("class", string.Empty)
                    let names = @class.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    where names.Contains(query)
                    select n,
                _ => new List<HtmlNode>()
            };
    }
}