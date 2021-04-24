using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Core;

namespace ScrapySharp.Extensions
{
    public static class CssQueryExtensions
    {
        public static IEnumerable<HtmlNode> CssSelect(this IEnumerable<HtmlNode> nodes, string expression)
        {
            return nodes.SelectMany(node => CssSelect(node, expression));
        }

        public static IEnumerable<HtmlNode> CssSelect(this HtmlNode node, string expression)
        {
            var tokenizer = new CssSelectorTokenizer();
            var tokens = tokenizer.Tokenize(expression);
            var executor = new CssSelectorExecutor<HtmlNode>(new List<HtmlNode> { node }, tokens.ToList(), new AgilityNavigationProvider());
            
            return executor.GetElements();
        }

        public static IEnumerable<HtmlNode> CssSelect(this HtmlNode node, string[] expressions)
        {
            // Use a HashSet to avoid duplicates.
            var elements = new HashSet<HtmlNode>();
            
            foreach (var expression in expressions)
            {
                var matchingElements = node.CssSelect(expression).ToList();

                foreach (var element in matchingElements)
                {
                    elements.Add(element);
                }
            }

            return elements;
        }

        public static IEnumerable<HtmlNode> CssSelectAncestors(this IEnumerable<HtmlNode> nodes, string expression)
        {
            var htmlNodes = nodes.SelectMany(node => CssSelectAncestors(node, expression)).ToArray();
            return htmlNodes.Distinct();
        }

        public static IEnumerable<HtmlNode> CssSelectAncestors(this HtmlNode node, string expression)
        {
            var tokenizer = new CssSelectorTokenizer();
            var tokens = tokenizer.Tokenize(expression);
            var executor = new CssSelectorExecutor<HtmlNode>(new List<HtmlNode> {node}, tokens.ToList(), new AgilityNavigationProvider())
            {
                MatchAncestors = true
            };

            return executor.GetElements();
        }

    }
}