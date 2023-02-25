using System.IO;
using NUnit.Framework;
using ScrapySharp.Html.Dom;

namespace ScrapySharp.Tests
{
    [TestFixture]
    public class When_generate_HTML_with_HDocument
    {
        [Test]
        public void When_generate_HTML_1()
        {
            var doc = new HDocument(
                new HElement("html", 
                    new HElement("body", 
                             new HElement("div", 
                                          new HAttribute("id", "login-box"), 
                                          new HElement("span", "Login:")),
                             new HElement("table",
                                          new HElement("tr", 
                                                       new HElement("td", "case1"))))
                ));

            var html = doc.GetOuterHtml(HtmlGenerationStyle.Indent);

            var html2 = File.ReadAllText("Html/GeneratedHtml1.htm");

            Assert.AreEqual(html2, html);
        }
    }
}