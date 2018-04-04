using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapySharp.Network
{
    public class RawRequest
    {
        internal RawRequest(string verb, Uri url, Version httpVersion, List<KeyValuePair<string, string>> headers, byte[] body, Encoding encoding)
        {
            Encoding = encoding;
            Verb = verb;
            Url = url;
            HttpVersion = httpVersion;
            Headers = headers;
            Body = body;
        }

        public string Verb { get; private set; }
        public Uri Url { get; private set; }
        public Version HttpVersion { get; private set; }
        public List<KeyValuePair<string, string>> Headers { get; private set; }
        public byte[] Body { get; private set; }
        public Encoding Encoding { get; private set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0} {1} HTTP/{2}.{3}\r\n", Verb, Url, HttpVersion.Major, HttpVersion.Minor);

            foreach (var header in Headers)
                builder.AppendFormat("{0}: {1}\r\n", header.Key, header.Value);

            builder.AppendFormat("\r\n");

            if (Body != null && Body.Length > 0)
                builder.AppendFormat("{0}\r\n", Encoding.ASCII.GetString(Body));

            return builder.ToString();
        }
    }
}