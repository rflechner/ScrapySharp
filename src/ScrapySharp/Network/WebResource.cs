using System;
using System.IO;

namespace ScrapySharp.Network
{
    public class WebResource : IDisposable
    {
        public WebResource(Stream content, string lastModified, Uri absoluteUrl, bool forceDownload, string contentType)
        {
            Content = content;
            LastModified = lastModified;
            AbsoluteUrl = absoluteUrl;
            ForceDownload = forceDownload;
            ContentType = contentType;
        }

        public void Dispose()
        {
            Content.Dispose();
        }

        public Stream Content { get; }

        public string LastModified { get; }

        public Uri AbsoluteUrl { get; }

        public bool ForceDownload { get; }

        public string ContentType { get; }

        public string GetTextContent()
        {
            Content.Position = 0;
            using var reader = new StreamReader(Content);
            return reader.ReadToEnd();
        }
    }
}