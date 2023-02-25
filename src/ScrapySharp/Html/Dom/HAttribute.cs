namespace ScrapySharp.Html.Dom
{
    public class HAttribute : IHSubContainer
    {
        public HAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}