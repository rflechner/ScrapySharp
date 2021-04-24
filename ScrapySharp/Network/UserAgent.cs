namespace ScrapySharp.Network
{
    public class UserAgent
    {
        public UserAgent(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public string Value { get; }

        public override string ToString() => Value;
    }
}