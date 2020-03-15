using System;
using System.IO;

namespace ScrapySharp.Network
{
    public class WebResource : IDisposable
    {
        private readonly Stream content;
        private readonly string lastModified;
        private readonly Uri absoluteUrl;
        private readonly bool forceDownload;
        private readonly string contentType;

        public WebResource(Stream content, string lastModified, Uri absoluteUrl, bool forceDownload, string contentType)
        {
            this.content = content;
            this.lastModified = lastModified;
            this.absoluteUrl = absoluteUrl;
            this.forceDownload = forceDownload;
            this.contentType = contentType;
        }

        public void Dispose()
        {
            content.Dispose();
        }

        public Stream Content
        {
            get { return content; }
        }

        public string LastModified
        {
            get { return lastModified; }
        }

        public Uri AbsoluteUrl
        {
            get { return absoluteUrl; }
        }

        public bool ForceDownload
        {
            get { return forceDownload; }
        }

        public string ContentType
        {
            get { return contentType; }
        }

        public string GetTextContent()
        {
            content.Position = 0;
            using (var reader = new StreamReader(content))
                return reader.ReadToEnd();
        }
    }
}