// ReSharper disable InconsistentNaming

using System.Linq;
using HtmlAgilityPack;
using NUnit.Framework;
using ScrapySharp.Extensions;

namespace ScrapySharp.Tests
{
    [TestFixture]
    public class When_parses_using_CssSelector
    {
        private readonly HtmlNode html;

        public When_parses_using_CssSelector()
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.Load(@"Html/Page1.htm");
            html = htmlDocument.DocumentNode;
        }

        [Test]
        public void When_css_class_contains_no_alpha_numerics()
        {
            var spans = html.CssSelect("span.login-box").ToArray();

            Assert.AreEqual(1, spans.Length);
        }

        [Test]
        public void When_id_contains_no_alpha_numerics()
        {
            var spans = html.CssSelect("span#pass-box").ToArray();

            Assert.AreEqual(1, spans.Length);
        }

        [Test]
        public void When_uses_simple_tagName()
        {
            var divs = html.CssSelect("div").ToArray();

            Assert.AreEqual(29, divs.Length);
        }

        [Test]
        public void When_uses_tagName_with_css_class()
        {
            Assert.AreEqual(3, html.CssSelect("div.content").Count());

            Assert.AreEqual(1, html.CssSelect("div.widget.monthlist").Count());
        }

        [Test]
        public void When_uses_tagName_with_css_class_using_inheritance()
        {
            Assert.AreEqual(1, html.CssSelect("div.left-corner div.node").Count());

            var nodes = html.CssSelect("span#testSpan span").ToArray();

            Assert.AreEqual(2, nodes.Length);

            Assert.AreEqual("tototata", nodes[0].InnerText);
            Assert.AreEqual("tata", nodes[1].InnerText);

        }

        [Test]
        public void When_uses_id()
        {
            Assert.AreEqual(1, html.CssSelect("#postPaging").Count());

            Assert.AreEqual(1, html.CssSelect("div#postPaging").Count());

            Assert.AreEqual(1, html.CssSelect("div#postPaging.testClass").Count());
        }

        [Test]
        public void When_uses_tagName_with_css_class_using_direct_inheritance()
        {
            Assert.AreEqual(1, html.CssSelect("div.content > p.para").Count());
        }

        [Test]
        public void When_uses_tagName_with_id_class_using_direct_inheritance()
        {
            Assert.AreEqual(1, html.CssSelect("ul#pagelist > li#listItem1").Count());
        }

        [Test]
        public void When_uses_ancestor()
        {
            var ancestors = html.CssSelect("p.para").CssSelectAncestors("div div.menu").ToArray();
            Assert.AreEqual(1, ancestors.Count());
        }

        [Test]
        public void When_uses_direct_ancestor()
        {
            var ancestors1 = html.CssSelect("p.para").CssSelectAncestors("div.content > div.menu").ToArray();
            Assert.AreEqual(0, ancestors1.Count());

            var ancestors2 = html.CssSelect("p.para").CssSelectAncestors("div.content > div.widget").ToArray();
            Assert.AreEqual(1, ancestors2.Count());
        }

        [Test]
        public void When_uses_attribute_selector()
        {
            Assert.AreEqual(1, html.CssSelect("input[type=button]").Count());
            
            Assert.AreEqual(2, html.CssSelect("input[type=text]").Count());

            Assert.AreEqual(10, html.CssSelect("script[type=text/javascript]").Count());

            Assert.AreEqual(2, html.CssSelect("link[type=application/rdf+xml]").Count());
        }

        [Test]
        public void When_uses_attribute_selector_with_css_class()
        {
            Assert.AreEqual(1, html.CssSelect("input[type=text].login").Count());
        }

        [Test]
        public void When_using_starts_with_attribute_selector()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(@"<html><body><hr /><hr id='bla123'/><hr id='1nothing'/><hr id='2nothing'/></body></html>");
            var node = doc.DocumentNode;

            var result = node.CssSelect("hr[id^=bla]").ToArray();

            Assert.AreEqual(result.Length, 1);
        }

        [Test]
        public void When_using_ends_with_attribute_selector()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(@"<html><body><hr /><hr id='bla123'/><hr id='1nothing'/><hr id='2nothing'/></body></html>");
            var node = doc.DocumentNode;

            var result = node.CssSelect("hr[id$=ing]").ToArray();

            Assert.AreEqual(result.Length, 2);
        }

        [Test]
        public void When_chain_methods()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(@"<html><body><table>"
                + "<tr><td>Case 1</td><td>Case 2</td><td>Case 3</td></tr>"
                + "<tr><td>Case 4</td><td>Case 5</td><td>Case 6</td></tr>"
                +"</table></body></html>");
            var node = doc.DocumentNode;

            var trs1 = node.CssSelect("tr").ToArray();
            Assert.AreEqual(2, trs1.Length);

            var tds1 = node.CssSelect("td").ToArray();
            Assert.AreEqual(6, tds1.Length);

            
            var tds2 = trs1[1].CssSelect("td").ToArray();
            Assert.AreEqual(3, tds2.Length);
        }

        [Test]
        public void When_select_ancestors()
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(@"<html><body><table>"
                         + "<tr><td>Case 1</td><td><label for=c1>Case 2</label></td><td>Case 3</td></tr>"
                         + "<tr><td>Case 4</td><td>Case 5</td><td>Case 6</td></tr>"
                         + "</table></body></html>");
            var html = doc.DocumentNode;

            var labels = html.CssSelect("label[for=c1]").ToArray();
            var trs = labels.CssSelectAncestors("tr").ToArray();

            Assert.AreEqual(1, trs.Length);
        }
    }
}

// ReSharper restore InconsistentNaming
