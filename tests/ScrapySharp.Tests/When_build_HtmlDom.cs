// ReSharper disable InconsistentNaming

using System.IO;
using NUnit.Framework;
using ScrapySharp.Html.Dom;
using ScrapySharp.Html.Parsing;
using System.Linq;

namespace ScrapySharp.Tests
{
    [TestFixture]
    public class When_build_HtmlDom
    {
        [Test]
        public void When_read_a_simple_tag()
        {
            var sourceCode = " dada\n<div class=\"login box1\" id=\"div1\" data-tooltip=\"salut, ça va?\">login: \n\t romcy</div>"
                             + "<img src=\"http://popo.fr/titi.gif\" />";

            var codeReader = new CodeReader(sourceCode);
            var declarationReader = new HtmlDeclarationReader(codeReader);
            var domBuilder = new HtmlDomBuilder(declarationReader);

            var elements = domBuilder.BuildDom().ToList();

            Assert.AreEqual(3, elements.Count);

            Assert.AreEqual(" dada\n", elements[0].InnerText);
            Assert.IsEmpty(elements[0].Name);

            Assert.AreEqual("div", elements[1].Name);
            Assert.AreEqual(3, elements[1].Attributes.Count);
            Assert.AreEqual("login box1", elements[1].Attributes["class"]);
            Assert.AreEqual("div1", elements[1].Attributes["id"]);
            Assert.AreEqual("salut, ça va?", elements[1].Attributes["data-tooltip"]);
            
            Assert.AreEqual("login: \n\t romcy", elements[1].InnerText);
            
            Assert.AreEqual(1, elements[1].Children.Count);
            Assert.IsEmpty(elements[1].Children[0].Name);
            Assert.AreEqual("login: \n\t romcy", elements[1].Children[0].InnerText);

            Assert.AreEqual("img", elements[2].Name);
            Assert.AreEqual(1, elements[2].Attributes.Count);
            Assert.AreEqual("http://popo.fr/titi.gif", elements[2].Attributes["src"]);
            Assert.AreEqual(0, elements[2].Children.Count);
        }

        [Test]
        public void When_parsing_ValidPage1()
        {
            var source = File.ReadAllText("Html/ValidPage1.htm");

            TestWordsReading(source);

            var codeReader = new CodeReader(source);
            var declarationReader = new HtmlDeclarationReader(codeReader);

            var tag = declarationReader.ReadTagDeclaration();
            Assert.AreEqual("html", tag.Name);
            Assert.AreEqual(DeclarationType.OpenTag, tag.Type);

            tag = declarationReader.ReadTagDeclaration();
            Assert.AreEqual(null, tag.Name);
            Assert.AreEqual(DeclarationType.TextElement, tag.Type);
            Assert.AreEqual("\r\n    ", tag.InnerText);

            tag = declarationReader.ReadTagDeclaration();
            Assert.AreEqual("div", tag.Name);
            Assert.AreEqual(DeclarationType.OpenTag, tag.Type);

            tag = declarationReader.ReadTagDeclaration();
            Assert.AreEqual(null, tag.Name);
            Assert.AreEqual(DeclarationType.TextElement, tag.Type);
            Assert.AreEqual("test", tag.InnerText);

            tag = declarationReader.ReadTagDeclaration();
            Assert.AreEqual("div", tag.Name);
            Assert.AreEqual(DeclarationType.CloseTag, tag.Type);


            var document = HDocument.Parse(source);

            Assert.AreEqual(1, document.Elements("html").Count());
            Assert.AreEqual(2, document.Elements("html").Elements("div").Count());

            Assert.AreEqual("test", document.Elements("html").Elements("div").ElementAt(0).InnerText);

            Assert.AreEqual(2, document.Elements("html").Elements("div").ElementAt(1).Elements("span").Count());
            Assert.AreEqual("Welcome", document.Elements("html").Elements("div").ElementAt(1).Elements("span").ElementAt(0).InnerText);
            Assert.AreEqual(" toto", document.Elements("html").Elements("div").ElementAt(1).Elements("span").ElementAt(1).InnerText);

            Assert.AreEqual(2, document.Descendants("div").Count());
            Assert.AreEqual(2, document.Descendants("span").Count());
        }

        [Test]
        public void When_parsing_InvalidPage1()
        {
            var source = File.ReadAllText("Html/InvalidPage1.htm");
            
            var document = HDocument.Parse(source);

            var doctype = document.Elements("DOCTYPE").Single();

            Assert.AreEqual("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">",
                doctype.InnerText);

            Assert.AreEqual(1, document.Elements("html").Count());
            Assert.AreEqual(2, document.Elements("html").Elements("div").Count());

            var firstDiv = document.Elements("html").Elements("div").First();
            Assert.AreEqual("test", firstDiv.InnerText);
            Assert.AreEqual("login id=", firstDiv.Attributes["class"]);

            Assert.AreEqual(2, document.Elements("html").Elements("div").ElementAt(1).Elements("span").Count());
            Assert.AreEqual("Welcome", document.Elements("html").Elements("div").ElementAt(1).Elements("span").ElementAt(0).InnerText);
            Assert.AreEqual(" toto", document.Elements("html").Elements("div").ElementAt(1).Elements("span").ElementAt(1).InnerText);

            Assert.AreEqual(2, document.Descendants("div").Count());
            Assert.AreEqual(2, document.Descendants("span").Count());

            Assert.AreEqual(1, document.Descendants("table").Count());
            Assert.AreEqual(1, document.Descendants("thead").Count());
            Assert.AreEqual(1, document.Descendants("tbody").Count());
            
            Assert.AreEqual(3, document.Descendants("tr").Count());
            Assert.AreEqual(4, document.Descendants("td").Count());

        }

        private static void TestWordsReading(string source)
        {
            var codeReader = new CodeReader(source);

            var word = codeReader.ReadWord();
            Assert.AreEqual("<", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual("html", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual(">", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual("\r", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual("\n", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual(" ", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual(" ", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual(" ", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual(" ", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual("<", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual("div", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual(" ", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual("class", word.Value);
            word = codeReader.ReadWord();
            Assert.AreEqual("=", word.Value);
            word = codeReader.ReadWord();
            Assert.AreEqual("login", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual(" ", word.Value);
            word = codeReader.ReadWord();
            Assert.AreEqual("id", word.Value);
            word = codeReader.ReadWord();
            Assert.AreEqual("=", word.Value);
            word = codeReader.ReadWord();
            Assert.AreEqual("lol", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual(">", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual("test", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual("<", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual("/", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual("div", word.Value);

            word = codeReader.ReadWord();
            Assert.AreEqual(">", word.Value);
        }
    }
}

// ReSharper restore InconsistentNaming