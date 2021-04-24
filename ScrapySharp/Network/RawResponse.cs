using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace ScrapySharp.Network
{
    public sealed class RawResponse
    {
        internal RawResponse(Version httpVersion, HttpStatusCode statusCode, string statusDescription, Header[] headers, byte[] body, Encoding encoding)
        {
            Encoding = encoding;
            HttpVersion = httpVersion;
            StatusCode = (int)statusCode;
            StatusDescription = statusDescription;
            Body = body;
            Headers = headers;
        }

        public Version HttpVersion { get; }
        public int StatusCode { get; }
        public string StatusDescription { get; }
        public Header[] Headers { get; }
        public byte[] Body { get; }
        public Encoding Encoding { get; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("HTTP/{0}.{1} {2} {3}\r\n", HttpVersion.Major, HttpVersion.Minor, StatusCode, StatusDescription);

            foreach (var header in Headers)
            {
                foreach (var value in header.Values)
                {
                    builder.AppendFormat("{0}: {1}\r\n", header.Name, value);
                }
            }
            
            builder.AppendFormat("\r\n");

            if (Body != null && Body.Length > 0)
                builder.AppendFormat("{0}\r\n", Encoding.ASCII.GetString(Body));

            return builder.ToString();
        }
    }
}