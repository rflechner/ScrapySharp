using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using ScrapySharp.Html.Dom;
using ScrapySharp.Network;

namespace ScrapySharp.Html.Forms
{
    public class WebForm
    {
        private HttpMethod method;
        private string action;

        public WebForm()
        {
            FormFields = new List<FormField>();
            method = HttpMethod.Post;
            action = string.Empty;
        }

        public WebForm(HtmlNode html)
        {
            var nodeParser = new AgilityNodeParser(html);
            Initialize(nodeParser);
        }

        public WebForm(HElement html)
        {
            var nodeParser = new HElementNodeParser(html);
            Initialize(nodeParser);
        }

        private void Initialize<T>(IHtmlNodeParser<T> nodeParser)
        {
            FormFields = ParseFormFields(nodeParser);
            ParseAction(nodeParser);
            ParseMethod(nodeParser);
        }

        private void ParseMethod<T>(IHtmlNodeParser<T> nodeParser)
        {
            var value = nodeParser.GetAttributeValue("method");

            if (!string.IsNullOrEmpty(value) && value.Equals("get"))
                method = HttpMethod.Get;
            else
                method = HttpMethod.Post;
        }

        private void ParseAction<T>(IHtmlNodeParser<T> nodeParser)
        {
            action = nodeParser.GetAttributeValue("action");
        }

        internal static List<FormField> ParseFormFields<T>(IHtmlNodeParser<T> node)
        {
            var inputs = from input in node.CssSelect("input")
                             let value = input.GetAttributeValue("value")
                             let type = input.GetAttributeValue("type")
                         where type != "checkbox" && type != "radio"
                         select new FormField
                         {
                             Name = input.GetAttributeValue("name"),
                             Value = string.IsNullOrEmpty(value) ? input.InnerText : value
                         };

            var checkboxes = from input in node.CssSelect("input[type=checkbox]")
                         let value = input.GetAttributeValue("value")
                             where input.Attributes.AllKeys.Contains("checked")
                         select new FormField
                         {
                             Name = input.GetAttributeValue("name"),
                             Value = string.IsNullOrEmpty(value) ? input.InnerText : value
                         };

            var radios = from input in node.CssSelect("input[type=radio]")
                         let value = input.GetAttributeValue("value")
                             where input.Attributes.AllKeys.Contains("checked")
                         select new FormField
                         {
                             Name = input.GetAttributeValue("name"),
                             Value = string.IsNullOrEmpty(value) ? input.InnerText : value
                         };

            var selects = from @select in node.CssSelect("select")
                          let name = @select.GetAttributeValue("name")
                          let option =
                              @select.CssSelect("option").FirstOrDefault(o => o.Attributes["selected"] != null) ??
                              @select.CssSelect("option").FirstOrDefault()
                          let value = option.GetAttributeValue("value")
                          select new FormField
                          {
                              Name = name,
                              Value = string.IsNullOrEmpty(value) ? option.InnerText : value
                          };

            return inputs.Concat(selects).Concat(checkboxes).Concat(radios).ToList();
        }

        public List<FormField> FormFields { get; set; }

        public string SerializeFormFields()
        {
            var builder = new StringBuilder();
            var fields = FormFields.ToArray();

            for (int i = 0; i < fields.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(fields[i].Name))
                    continue;

                if (i > 0)
                    builder.Append('&');
                builder.AppendFormat("{0}={1}", Uri.EscapeDataString(fields[i].Name), Uri.EscapeDataString(fields[i].Value));
            }

            return builder.ToString();
        }

        public string this[string key]
        {
            get
            {
                var field = FormFields.FirstOrDefault(f => f.Name == key);
                return field != null ? field.Value : null;
            }
            set
            {
                var field = FormFields.FirstOrDefault(f => f.Name == key);
                if (field != null)
                    FormFields.Remove(field);

                FormFields.Add(new FormField { Name = key, Value = value });
            }
        }

        public void Submit(ScrapingBrowser browser, Uri url, HttpMethod verb)
        {
            browser.NavigateTo(url, verb, SerializeFormFields());
        }

        public void Submit(ScrapingBrowser browser, Uri url)
        {
            browser.NavigateTo(url, method, SerializeFormFields());
        }

        public HttpMethod Method
        {
            get { return method; }
            set { method = value; }
        }

        public string Action
        {
            get { return action; }
            set { action = value; }
        }
    }
}