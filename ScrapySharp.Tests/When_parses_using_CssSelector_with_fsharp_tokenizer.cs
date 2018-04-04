// ReSharper disable InconsistentNaming

using System.Linq;
using HtmlAgilityPack;
using NUnit.Framework;
using ScrapySharp.Core;
using ScrapySharp.Extensions;
using ScrapySharp.Core;

namespace ScrapySharp.Tests
{
    [TestFixture]
    public class When_parses_using_CssSelector_with_fsharp_tokenizer
    {
        private readonly HtmlNode html;

        public When_parses_using_CssSelector_with_fsharp_tokenizer()
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.Load(@"Html/Page1.htm");
            html = htmlDocument.DocumentNode;
        }

        [Test]
        public void When_css_class_contains_no_alpha_numerics()
        {
            var tokenizer = new CssSelectorTokenizer();
            var tokens = tokenizer.Tokenize("span.loginbox");

            Assert.AreEqual(3, tokens.Length);

            tokens = tokenizer.Tokenize("span.login-box");

            Assert.AreEqual(3, tokens.Length);
        }

        [Test]
        public void When_execute_css_selector1()
        {
            var tokenizer = new CssSelectorTokenizer();
            var tokens = tokenizer.Tokenize("span.login-box");
            Assert.AreEqual(3, tokens.Length);

            var executor = new CssSelectorExecutor<HtmlNode>(html.ChildNodes.ToList(), tokens.ToList(), new AgilityNavigationProvider());
            HtmlNode[] htmlNodes = executor.GetElements();

            Assert.AreEqual(1, htmlNodes.Length);
        }

        [Test]
        public void When_execute_css_selector2()
        {
            var tokenizer = new CssSelectorTokenizer();
            var tokens = tokenizer.Tokenize("div.widget.monthlist");
            Assert.AreEqual(5, tokens.Length);

            var executor = new CssSelectorExecutor<HtmlNode>(html.ChildNodes.ToList(), tokens.ToList(), new AgilityNavigationProvider());
            HtmlNode[] htmlNodes = executor.GetElements();

            Assert.AreEqual(1, htmlNodes.Length);
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
            var cssSelect1 = html.CssSelect("div.content > p.para").ToArray();
            var CssSelect = html.CssSelect("div.content p.para").ToArray();

            Assert.AreEqual(1, cssSelect1.Count());
            Assert.AreEqual(2, CssSelect.Count());
        }

        [Test]
        public void When_uses_tagName_with_id_class_using_direct_inheritance()
        {
            Assert.AreEqual(1, html.CssSelect("ul#pagelist > li#listItem1").Count());
        }

        [Test]
        public void When_uses_ancestor()
        {
            var cssSelect1 = html.CssSelect("p.para");

            var ancestors = cssSelect1.CssSelectAncestors("div div.menu").ToArray();
            
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
    }
}

// ReSharper restore InconsistentNaming