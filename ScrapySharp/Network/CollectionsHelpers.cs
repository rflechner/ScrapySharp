using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace ScrapySharp.Network
{
    public static class CollectionsHelpers
    {
        public static string ToHttpVars(this NameValueCollection variables)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < variables.Count; i++)
            {
                var key = variables.GetKey(i);
                var values = variables.GetValues(i);
                if (values != null)
                    foreach (var value in values)
                        builder.AppendFormat("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value));
                if (i < variables.Count - 1)
                    builder.Append("&");
            }

            return builder.ToString();
        }   
    }
}