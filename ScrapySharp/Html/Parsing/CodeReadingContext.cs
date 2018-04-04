namespace ScrapySharp.Html.Parsing
{
    public enum CodeReadingContext
    {
        None,
        SearchingTag,
        InBeginTag,
        InTagContent,
        InTagEnd,
        InAttributeName,
        InAttributeValue,
        InQuotes
    }
}