namespace ScrapySharp.Html.Dom
{
    public class HComment : HElement
    {
        public override string GetOuterHtml(HtmlGenerationStyle generationStyle = HtmlGenerationStyle.None)
        {
            return string.Format("<!--{0}-->", innerText);
        }
    }
}