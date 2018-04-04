namespace ScrapySharp.Network
{
    public class FakeUserAgent
    {
        private string name;
        private string userAgent;

        public FakeUserAgent(string name, string userAgent)
        {
            this.name = name;
            this.userAgent = userAgent;
        }

        public string Name
        {
            get { return name; }
        }

        public string UserAgent
        {
            get { return userAgent; }
        }
    }
}