using System.Globalization;
using ScrapySharp.Html.Parsing;

namespace ScrapySharp.Extensions
{
    public static class TokenHelper
    {
        public static bool IsToken(this Word word)
        {
            return IsToken(word.Value);
        }
        
        public static bool IsToken(this char c)
        {
            return IsToken(c.ToString(CultureInfo.InvariantCulture));
        }

        public static bool IsToken(this string value)
        {
            return value == Tokens.CloseTag ||
                   value == Tokens.CommentBegin ||
                   value == Tokens.CommentEnd ||
                   value == Tokens.Quote.ToString(CultureInfo.InvariantCulture) ||
                   value == Tokens.SimpleQuote.ToString(CultureInfo.InvariantCulture) ||
                   value == Tokens.TagBegin.ToString(CultureInfo.InvariantCulture) ||
                   value == Tokens.TagEnd.ToString(CultureInfo.InvariantCulture) ||
                   value == Tokens.Doctype ||
                   value == Tokens.CloseTagDeclarator;
        }
    }
}