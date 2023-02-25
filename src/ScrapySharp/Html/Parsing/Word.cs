using System.Linq;

namespace ScrapySharp.Html.Parsing
{
    public class Word
    {
        private readonly string value;
        private readonly int lineNumber;
        private readonly int linePositionEnd;
        private readonly bool isQuoted;
        private readonly bool isWhiteSpace;

        public Word(string value, int lineNumber, int linePositionEnd, bool isQuoted)
        {
            this.value = value;
            this.lineNumber = lineNumber;
            this.linePositionEnd = linePositionEnd;
            this.isQuoted = isQuoted;

            isWhiteSpace = !string.IsNullOrEmpty(value) && value.All(char.IsWhiteSpace);
        }

        public string Value
        {
            get
            {
                return value;
            }
        }

        public string QuotedValue
        {
            get
            {
                if (IsQuoted)
                    return '"' + value + '"';

                return value;
            }
        }

        public int LineNumber
        {
            get { return lineNumber; }
        }

        public int LinePositionEnd
        {
            get { return linePositionEnd; }
        }

        public int LinePositionBegin
        {
            get { return linePositionEnd - value.Length; }
        }

        public bool IsQuoted
        {
            get { return isQuoted; }
        }

        public bool IsWhiteSpace
        {
            get { return isWhiteSpace; }
        }

        public static implicit operator string(Word word)
        {
            return word.Value;
        }

        public static implicit operator char(Word word)
        {
            if (string.IsNullOrEmpty(word.Value))
                return default(char);

            return word.Value.FirstOrDefault();
        }

    }
}