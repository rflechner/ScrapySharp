using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ScrapySharp.Core;
using ScrapySharp.Html.Dom;

namespace ScrapySharp.Extensions
{
    public static class HDocumentCssQueryExtensions
    {
        public static IEnumerable<HElement> CssSelect(this HDocument doc, string expression)
        {
            var hElement = new HElement
                {
                    Children = doc.Children
                };

            return hElement.CssSelect(expression);
        }

        public static IEnumerable<HElement> CssSelect(this IEnumerable<HElement> nodes, string expression)
        {
            return nodes.SelectMany(node => CssSelect(node, expression));
        }

        public static IEnumerable<HElement> CssSelectAncestors(this IEnumerable<HElement> nodes, string expression)
        {
            return nodes.SelectMany(node => CssSelectAncestors(node, expression)).Distinct();
        }

        public static IEnumerable<HElement> CssSelectAncestors(this HElement node, string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return new HElement[] { };

            var tokenizer = new CssSelectorTokenizer();
            var tokens = tokenizer.Tokenize(expression);
            var executor = new CssSelectorExecutor<HElement>(new List<HElement> { node }, tokens.ToList(), new HElementNavigationProvider());
            executor.MatchAncestors = true;

            return executor.GetElements();
        }

        public static IEnumerable<HElement> CssSelect(this HElement node, string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return new HElement[] { };

            var tokenizer = new CssSelectorTokenizer();
            var tokens = tokenizer.Tokenize(expression);
            var executor = new CssSelectorExecutor<HElement>(new List<HElement> { node }, tokens.ToList(), new HElementNavigationProvider());

            return executor.GetElements();
        }
    }
}