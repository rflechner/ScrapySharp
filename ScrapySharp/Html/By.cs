using System;
using HtmlAgilityPack;

namespace ScrapySharp.Html
{
    public class By
    {
        internal string Query { get; private set; }
        internal ElementSearchKind SearchKind { get; private set; }
        internal StringComparison ComparisonType { get; private set; }

        private By(string query, ElementSearchKind searchKind, StringComparison comparisonType)
        {
            Query = query;
            SearchKind = searchKind;
            ComparisonType = comparisonType;
        }

        internal ElementFinder CreateElementFinder(HtmlNode html, string tagName)
        {
            return new ElementFinder(html, SearchKind, tagName, Query, ComparisonType);
        }

        public static By Id(string query, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return new By(query, ElementSearchKind.Id, comparisonType);
        }

        public static By Name(string query, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return new By(query, ElementSearchKind.Name, comparisonType);
        }

        public static By Text(string query, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return new By(query, ElementSearchKind.Text, comparisonType);
        }

        public static By Class(string query, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return new By(query, ElementSearchKind.Class, comparisonType);
        }
    }
}