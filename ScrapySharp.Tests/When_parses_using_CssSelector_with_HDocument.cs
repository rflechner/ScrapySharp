using System.IO;
using System.Linq;
using NUnit.Framework;
using ScrapySharp.Extensions;
using ScrapySharp.Html.Dom;

namespace ScrapySharp.Tests
{
    [TestFixture]
    public class When_parses_using_CssSelector_with_HDocument
    {
        public HDocument GetHtmlage1()
        {
            var source = File.ReadAllText("Html/Page1.htm");
            return HDocument.Parse(source);
        }

        [Test]
        public void When_css_class_contains_no_alpha_numerics()
        {
            var spans = GetHtmlage1().CssSelect("span.login-box").ToArray();

            Assert.AreEqual(1, spans.Length);
        }

        [Test]
        public void When_id_contains_no_alpha_numerics()
        {
            var spans = GetHtmlage1().CssSelect("span#pass-box").ToArray();

            Assert.AreEqual(1, spans.Length);
        }

        [Test]
        public void When_uses_simple_tagName()
        {
            var divs = GetHtmlage1().CssSelect("div").ToArray();

            Assert.AreEqual(29, divs.Length);
        }

        [Test]
        public void When_uses_tagName_with_css_class()
        {
            var html = GetHtmlage1();

            Assert.AreEqual(3, html.CssSelect("div.content").Count());

            Assert.AreEqual(1, html.CssSelect("div.widget.monthlist").Count());
        }

        [Test]
        public void When_uses_tagName_with_css_class_using_inheritance()
        {
            var html = GetHtmlage1();
            Assert.AreEqual(1, html.CssSelect("div.left-corner div.node").Count());

            var nodes = html.CssSelect("span#testSpan span").ToArray();

            Assert.AreEqual(2, nodes.Length);

            Assert.AreEqual("tototata", nodes[0].InnerText);
            Assert.AreEqual("tata", nodes[1].InnerText);

        }

        [Test]
        public void When_uses_id()
        {
            var html = GetHtmlage1();
            Assert.AreEqual(1, html.CssSelect("#postPaging").Count());

            Assert.AreEqual(1, html.CssSelect("div#postPaging").Count());

            Assert.AreEqual(1, html.CssSelect("div#postPaging.testClass").Count());
        }

        [Test]
        public void When_uses_tagName_with_css_class_using_direct_inheritance()
        {
            var html = GetHtmlage1();
            Assert.AreEqual(1, html.CssSelect("div.content > p.para").Count());
        }

        [Test]
        public void When_uses_tagName_with_id_class_using_direct_inheritance()
        {
            var html = GetHtmlage1();
            Assert.AreEqual(1, html.CssSelect("ul#pagelist > li#listItem1").Count());
        }

        [Test]
        public void When_uses_ancestor()
        {
            var html = GetHtmlage1();
            var ancestors = html.CssSelect("p.para").CssSelectAncestors("div div.menu").ToArray();
            Assert.AreEqual(1, ancestors.Count());
        }

        [Test]
        public void When_uses_direct_ancestor()
        {
            var html = GetHtmlage1();
            var ancestors1 = html.CssSelect("p.para").CssSelectAncestors("div.content > div.menu").ToArray();
            Assert.AreEqual(0, ancestors1.Count());

            var ancestors2 = html.CssSelect("p.para").CssSelectAncestors("div.content > div.widget").ToArray();
            Assert.AreEqual(1, ancestors2.Count());
        }

        [Test]
        public void When_uses_attribute_selector()
        {
            var html = GetHtmlage1();
            Assert.AreEqual(1, html.CssSelect("input[type=button]").Count());

            Assert.AreEqual(2, html.CssSelect("input[type=text]").Count());

            Assert.AreEqual(10, html.CssSelect("script[type=text/javascript]").Count());

            Assert.AreEqual(2, html.CssSelect("link[type=application/rdf+xml]").Count());
        }

        [Test]
        public void When_uses_attribute_selector_with_css_class()
        {
            var html = GetHtmlage1();
            Assert.AreEqual(1, html.CssSelect("input[type=text].login").Count());
        }

        [Test]
        public void When_using_starts_with_attribute_selector()
        {
            var source = "<html><body><hr /><hr id=\"bla123\"/><hr id=\"1nothing\"/><hr id=\"2nothing\"/></body></html>";
            var doc = HDocument.Parse(source);

            var result = doc.CssSelect("hr[id^=bla]").ToArray();

            Assert.AreEqual(result.Length, 1);

            Assert.AreEqual(1, doc.CssSelect("hr[id|=bla]").Count());
        }

        [Test]
        public void When_using_ends_with_attribute_selector()
        {
            var source = "<html><body><hr /><hr id='bla123'/><hr id=\"1nothing\"/><hr id='2nothing'/></body></html>";
            var doc = HDocument.Parse(source);

            var result = doc.CssSelect("hr[id$=ing]").ToArray();

            Assert.AreEqual(result.Length, 2);
        }

        [Test]
        public void When_using_selector_attribute_equals_with_spaces()
        {
            var source =
                "<html><body><hr /><hr id='bla123'/><hr id=\"1nothing\"/><hr id='2nothing' class=\"toto tata\"/></body></html>";
            var doc = HDocument.Parse(source);

            Assert.AreEqual(1, doc.CssSelect("hr.toto.tata").Count());
            Assert.AreEqual(1, doc.CssSelect("hr[class='toto tata']").Count());
        }

        [Test]
        public void When_using_attributeContains_selector()
        {
            var source = "<html><body><hr /><hr id=\"bla123\"/><hr id=\"1nothing\"/><hr id=\"2nothing\"/></body></html>";
            var doc = HDocument.Parse(source);

            Assert.AreEqual(2, doc.CssSelect("hr[id*=thi]").Count());
        }

        [Test]
        public void When_using_attributeContainsWord_selector()
        {
            // http://api.jquery.com/attribute-contains-word-selector/

            var source = @"<html>
<body>
  <input name=""man-news"" />

  <input name=""milk man"" />
  <input name=""letterman2"" />
  <input name=""newmilk"" />
</body>
</html>";
            var doc = HDocument.Parse(source);

            Assert.AreEqual(1, doc.CssSelect("input[name~='man']").Count());
        }

        [Test]
        public void When_using_attributeNotEqual_selector()
        {
            // http://api.jquery.com/attribute-not-equal-selector/

            var source = @"<html>
<body>
  <input name=""man-news"" />

  <input name=""milk man"" />
  <input name=""letterman2"" />
  <input name=""newmilk"" />
</body>
</html>";
            var doc = HDocument.Parse(source);

            Assert.AreEqual(3, doc.CssSelect("input[name!=man-news]").Count());
        }

        [Test]
        public void When_using_checkbox_selector()
        {
            // http://api.jquery.com/checkbox-selector/

            var source = @"<html>
<body>
  <input name=""man-news"" />

  <input name=""milk man"" />
  <input name=""letterman2"" />
  <input name=""newmilk"" />

  <input type=""checkbox"" />
  <input type=""file"" />
  <input type=""hidden"" />
</body>
</html>";
            var doc = HDocument.Parse(source);

            Assert.AreEqual(1, doc.CssSelect("input:checkbox").Count());
            Assert.AreEqual(1, doc.CssSelect(":checkbox").Count());
        }

        [Test]
        public void When_using_checked_selector()
        {
            // http://api.jquery.com/checkbox-selector/

            var source = @"<html>
<body>
  <input name=""man-news"" />

  <input name=""milk man"" />
  <input name=""letterman2"" />
  <input name=""newmilk"" />

  <input type=""checkbox"" checked />
  <input type=""checkbox"" />
  <input type=""checkbox"" checked=""checked"" />
  <input type=""file"" />
  <input type=""hidden"" />
</body>
</html>";
            var doc = HDocument.Parse(source);

            Assert.AreEqual(2, doc.CssSelect("input:checked").Count());
            Assert.AreEqual(2, doc.CssSelect(":checked").Count());
        }

        [Test]
        public void When_using_disabled_and_enabled_selector()
        {
            // http://api.jquery.com/disabled-selector/

            var source = @"<html>
<body>
  <input name=""man-news"" />

  <input name=""milk man"" />
  <input name=""letterman2"" />
  <input name=""newmilk"" />

  <input type=""checkbox"" disabled />
  <input type=""checkbox"" />
  <input type=""checkbox"" disabled=""disabled"" />
  <input type=""file"" />
  <input type=""hidden"" />
</body>
</html>";
            var doc = HDocument.Parse(source);

            Assert.AreEqual(2, doc.CssSelect("input:disabled").Count());
            Assert.AreEqual(2, doc.CssSelect(":disabled").Count());

            Assert.AreEqual(7, doc.CssSelect("input:enabled").Count());
        }

        [Test]
        public void When_using_selected_selector()
        {
            var source = @"<html>
<body>
  <select name=""group"" onChange=""form.submit()"">
	<option value='1'>Nissan</option>
	<option value=""2"" selected >Volvo</option>
	<option value='3'>BMW</option>
  </select>
</body>
</html>";
            var doc = HDocument.Parse(source);

            Assert.AreEqual(1, doc.CssSelect(":selected").Count());
            Assert.AreEqual(1, doc.CssSelect("select option:selected").Count());
        }
    }
}