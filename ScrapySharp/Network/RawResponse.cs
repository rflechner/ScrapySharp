using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace ScrapySharp.Network
{
    public class RawResponse
    {
        internal RawResponse(Version httpVersion, HttpStatusCode statusCode, string statusDescription, NameValueCollection headers, byte[] body, Encoding encoding)
        {
            Encoding = encoding;
            HttpVersion = httpVersion;
            StatusCode = (int)statusCode;
            StatusDescription = statusDescription;
            Body = body;
            Headers = headers.AllKeys.Select(k => new KeyValuePair<string, string>(k, headers[k])).ToList();
        }

        public Version HttpVersion { get; private set; }
        public int StatusCode { get; private set; }
        public string StatusDescription { get; private set; }
        public List<KeyValuePair<string, string>> Headers { get; private set; }
        public byte[] Body { get; private set; }
        public Encoding Encoding { get; private set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("HTTP/{0}.{1} {2} {3}\r\n", HttpVersion.Major, HttpVersion.Minor, StatusCode, StatusDescription);

            foreach (var header in Headers)
                builder.AppendFormat("{0}: {1}\r\n", header.Key, header.Value);
            builder.AppendFormat("\r\n");

            if (Body != null && Body.Length > 0)
                builder.AppendFormat("{0}\r\n", Encoding.ASCII.GetString(Body));

            return builder.ToString();
        }
    }
}