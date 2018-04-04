namespace ScrapySharp.Cache
{
    public class WebResourceEntity
    {
        public string LastModified { get; set; }

        public string AbsoluteUrl { get; set; }

        public bool ForceDownload { get; set; }
    }
}