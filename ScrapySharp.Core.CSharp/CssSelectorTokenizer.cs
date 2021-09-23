using Microsoft.FSharp.Collections;
namespace ScrapySharp.Core.CSharp
{
    public class CssSelectorTokenizer
    {
        public CssSelectorTokenizer()
        {
            this.charCount = 0;
            this.source = FSharpList<char>.Empty;
            this.cssSelector = "";
            this.inQuotes = false;
        }
        public Token[] Tokenize(string pCssSelector)
        {
            this.cssSelector = pCssSelector;
            this.source = ArrayModule.ToList<char>(this.cssSelector.ToCharArray());
            this.charCount = this.source.Length;
            return ListModule.ToArray<Token>(this.tokenize());
        }
        internal FSharpList<Token> tokenize()
        {
            return CssSelectorTokenizerN.tokenize60(this, FSharpList<Token>.Empty, this.source);
        }
        internal int getOffset(FSharpList<char> t)
        {
            return this.charCount - 1 - t.Length;
        }
        internal int charCount;
        internal FSharpList<char> source;
        internal string cssSelector;
        internal bool inQuotes;
    }
}
