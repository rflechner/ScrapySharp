// ReSharper disable InconsistentNaming

using NUnit.Framework;
using ScrapySharp.Html.Dom;
using ScrapySharp.Html.Parsing;

namespace ScrapySharp.Tests
{
    [TestFixture]
    public class When_use_HtmlDeclarationReader
    {
        [Test]
        public void When_read_a_simple_tag()
        {
            var sourceCode = " dada\n<div class=\"login box1\" id=\"div1\" data-tooltip=\"salut, ça va?\">login: \n\t romcy</div>"
                + "<img src=\"http://popo.fr/titi.gif\" />";

            var codeReader = new CodeReader(sourceCode);
            var declarationReader = new HtmlDeclarationReader(codeReader);

            var element = declarationReader.ReadTagDeclaration();
            Assert.AreEqual(" dada\n", element.InnerText);
            Assert.AreEqual(DeclarationType.TextElement, element.Type);
            
            element = declarationReader.ReadTagDeclaration();
            Assert.AreEqual("div", element.Name);
            Assert.AreEqual(DeclarationType.OpenTag, element.Type);
            Assert.AreEqual(3, element.Attributes.Count);
            Assert.AreEqual("login box1", element.Attributes["class"]);
            Assert.AreEqual("div1", element.Attributes["id"]);
            Assert.AreEqual("salut, ça va?", element.Attributes["data-tooltip"]);
            
            element = declarationReader.ReadTagDeclaration();
            Assert.IsNull(element.Name);
            Assert.AreEqual("login: \n\t romcy", element.InnerText);
            Assert.AreEqual(DeclarationType.TextElement, element.Type);

            element = declarationReader.ReadTagDeclaration();
            Assert.AreEqual("div", element.Name);
            Assert.AreEqual(DeclarationType.CloseTag, element.Type);

            element = declarationReader.ReadTagDeclaration();
            Assert.AreEqual("img", element.Name);
            Assert.AreEqual(DeclarationType.SelfClosedTag, element.Type);
            Assert.AreEqual("http://popo.fr/titi.gif", element.Attributes["src"]);

            element = declarationReader.ReadTagDeclaration();
            Assert.IsNull(element);
        }
    }
}

// ReSharper restore InconsistentNaming