// ReSharper disable InconsistentNaming

using NUnit.Framework;
using ScrapySharp.Core;

namespace ScrapySharp.Tests
{
    [TestFixture]
    public class When_tokenize_CssSelector
    {
        [Test]
        public void When_tokenize_selectors_used_in_v1()
        {
            var tokenizer = new CssSelectorTokenizer();
            var tokens = tokenizer.Tokenize("span.loginbox");
            Assert.AreEqual(3, tokens.Length);

            tokens = tokenizer.Tokenize("span.login-box");
            Assert.AreEqual(3, tokens.Length);

            tokens = tokenizer.Tokenize("script[type=text/javascript]");
            Assert.AreEqual(6, tokens.Length);

            tokens = tokenizer.Tokenize("hr[id^=bla]");
            Assert.AreEqual(6, tokens.Length);

            tokens = tokenizer.Tokenize("hr[id$=ing]");
            Assert.AreEqual(6, tokens.Length);

            tokens = tokenizer.Tokenize("link[type=application/rdf+xml]");
            Assert.AreEqual(6, tokens.Length);
        }
    }
}

// ReSharper restore InconsistentNaming