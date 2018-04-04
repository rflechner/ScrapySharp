namespace ScrapySharp.Html.Parsing
{
    public class Tokens
    {
        public const char TagBegin = '<';
        public const char TagEnd = '>';

        public const char Quote = '\"';
        public const char SimpleQuote = '\'';

        public const string CloseTag = "/";

        public const string CommentBegin = "!--";
        public const string CommentEnd = "--";
        
        public const string Assign = "=";

        public const string CloseTagDeclarator = "</";

        public const string Doctype = "<!";
    }
}
