using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using ScrapySharp.Html.Dom;

namespace ScrapySharp.Extensions
{
    public static class HtmlParsingHelper
    {
        private static Regex spacesRegex = new Regex("[ ]+", RegexOptions.Compiled);
        private static Regex asciiRegex = new Regex("(([=][0-9A-F]{0,2})+)|([ ]+)", RegexOptions.Compiled);

        /// <summary>
        /// Convert a string to a date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DateTime ToDate(this string value) => Convert.ToDateTime(value);

        /// <summary>
        /// Convert a string to a date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static DateTime ToDate(this string value, string format) => ToDate(value, format, CultureInfo.InvariantCulture);

        /// <summary>
        /// Convert a string to a date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <returns></returns>
        public static DateTime ToDate(this string value, string format, CultureInfo cultureInfo)
            => DateTime.TryParseExact(value, format, cultureInfo, DateTimeStyles.None, out var result) ? result : DateTime.MinValue;

        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string GetAttributeValue(this HtmlNode node, string name) => node.GetAttributeValue(name, string.Empty);

        /// <summary>
        /// Convert string value to HTML node.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static HtmlNode ToHtmlNode(this string content)
        {
            var document = new HtmlDocument();
            document.LoadHtml(content);

            return document.DocumentNode;
        }
        
        /// <summary>
        /// Convert WebResponse content to HTML node.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public static HtmlNode ToHtmlNode(this WebResponse response)
        {
            var document = new HtmlDocument();
            var html = string.Empty;;

            var responseStream = response.GetResponseStream();
            if (responseStream != null)
            {
                using var reader = new StreamReader(responseStream);
                html = reader.ReadToEnd();
            }

            document.LoadHtml(html);

            return document.DocumentNode;
        }
        
        /// <summary>
        /// Convert string value to HDocument.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static HDocument ToHDocument(this string content) => HDocument.Parse(content);

        /// <summary>
        /// Convert WebResponse content to HDocument.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public static HDocument ToHDocument(this WebResponse response)
        {
            string html;

            var responseStream = response.GetResponseStream();
            if (responseStream == null)
                html = string.Empty;
            else
                using (var reader = new StreamReader(responseStream))
                    html = reader.ReadToEnd();

            return html.ToHDocument();
        }

        /// <summary>
        /// Gets the next sibling with specified tag name.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static HtmlNode GetNextSibling(this HtmlNode node, string name)
        {
            var currentNode = node.NextSibling;

            while (currentNode.NextSibling != null && currentNode.Name != name)
                currentNode = currentNode.NextSibling;

            return currentNode.Name == name ? currentNode : null;
        }

        /// <summary>
        /// Gets the next table cell value.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        /// <param name="comparison">The comparison type.</param>
        /// <returns></returns>
        public static HtmlValue GetNextTableCellValue(this HtmlNode node, string name)
        {
            var results = GetNodesFollowedByValue(node, "td", name);
            if (!results.Any())
                return null;

            var innerText = results.Last().InnerText.CleanInnerHtmlAscii().CleanInnerText();
            if (innerText.StartsWith(":"))
                innerText = innerText.Substring(1).CleanInnerHtmlAscii().CleanInnerText();

            return innerText;
        }

        /// <summary>
        /// Gets the next table cell value.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        /// <param name="comparison">The comparison type.</param>
        /// <returns></returns>
        public static HtmlValue GetNextTableCellValue(this HtmlNode node, string name, NodeValueComparison comparison)
        {
            var results = GetNodesFollowedByValue(node, "td", name, comparison);
            if (!results.Any())
                return null;

            var innerText = results.Last().InnerText.CleanInnerHtmlAscii().CleanInnerText();
            if (innerText.StartsWith(":"))
                innerText = innerText.Substring(1).CleanInnerHtmlAscii().CleanInnerText();

            return innerText;
        }

        /// <summary>
        /// Gets the nodes followed by value.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        public static IReadOnlyCollection<HtmlNode> GetNodesFollowedByValue(this HtmlNode node, string name, string value, NodeValueComparison comparison = NodeValueComparison.Equals)
        {
            var comparer = new NodeValueComparer(comparison);
            var cleanName = value.CleanInnerText();
            return (from d in node.Descendants(name)
                    where comparer.Compare(d.InnerText.CleanInnerHtmlAscii().CleanInnerText(), cleanName)
                    select d.GetNextSibling(name)).ToArray();
        }

        /// <summary>
        /// Gets the nodes followed by value.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        public static IEnumerable<HtmlNode> GetNodesFollowedByValue(this IEnumerable<HtmlNode> nodes, string name, string value, NodeValueComparison comparison = NodeValueComparison.Equals)
        {
            return nodes.SelectMany(node => node.GetNodesFollowedByValue(name, value, comparison));
        }

        /// <summary>
        /// Gets the next table line value.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        /// <param name="comparison">The comparison type.</param>
        /// <returns></returns>
        public static HtmlValue GetNextTableLineValue(this HtmlNode node, string name, NodeValueComparison comparison = NodeValueComparison.Equals)
        {
            var results = GetNodesFollowedByValue(node, "tr", name, comparison);
            if (!results.Any())
                return null;

            var innerText = results.First().InnerText.CleanInnerHtmlAscii().CleanInnerText();
            if (innerText.StartsWith(":"))
                innerText = innerText.Substring(1).CleanInnerHtmlAscii().CleanInnerText();

            return innerText;
        }

        /// <summary>
        /// Cleans the inner HTML ASCII.
        /// </summary>
        /// <example>
        /// "text =09".CleanInnerHtmlAscii() returns "text "
        /// </example>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string CleanInnerHtmlAscii(this string expression)
        {
            var cleaned = expression.Replace("=C3=B4", "ô");
            cleaned = asciiRegex.Replace(cleaned, " ");

            return cleaned;
        }


        /// <summary>
        /// Cleans the inner text from excessive spaces characters.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string CleanInnerText(this string expression)
        {
            var cleaned = expression.Replace('\t', ' ').Replace('\r', ' ')
                .Replace('\n', ' ');

            cleaned = WebUtility.HtmlDecode(cleaned);
            
            return spacesRegex.Replace(cleaned, " ").Trim();
        }
    }
}