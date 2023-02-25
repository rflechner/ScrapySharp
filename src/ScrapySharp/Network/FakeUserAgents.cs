namespace ScrapySharp.Network
{
    public static class FakeUserAgents
    {
        public static readonly UserAgent ChromeForWindows = new UserAgent("Chrome Windows", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36");
        public static readonly UserAgent ChromeForMacOs = new UserAgent("Chrome Mac OS", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36");
        public static readonly UserAgent ChromeForIphone = new UserAgent("Chrome iPhone", "Mozilla/5.0 (iPhone; CPU iPhone OS 13_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) CriOS/81.0.4044.124 Mobile/15E148 Safari/604.1");
 
        public static readonly UserAgent InternetExplorer8 = new UserAgent("Internet Explorer 8", "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CMDTDF; .NET4.0C; .NET4.0E)");
        public static readonly UserAgent InternetExplorer11 = new UserAgent("Internet Explorer 11", "Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko");
    }
}