namespace ScrapySharp.Network
{
    public class Header
    {
        public string Name { get; }
        public string[] Values { get; }

        public Header(string name, string value) : this(name, new []{value})
        {
            
        }
        
        public Header(string name, string[] values)
        {
            Name = name;
            Values = values;
        }
    }
}