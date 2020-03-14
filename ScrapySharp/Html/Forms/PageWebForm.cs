using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using ScrapySharp.Network;
using ScrapySharp.Extensions;

namespace ScrapySharp.Html.Forms
{
    public class PageWebForm
    {
        private readonly HtmlNode html;
        private readonly ScrapingBrowser browser;
        private HttpVerb method;
        private string action;

        public PageWebForm(HtmlNode html, ScrapingBrowser browser)
        {
            this.html = html;
            this.browser = browser;
            Initialize();
        }

        private void Initialize()
        {
            var nodeParser = new AgilityNodeParser(html);
            ParseAction(nodeParser);
            ParseMethod(nodeParser);

            FormFields = ParseFormFields(nodeParser);

            if (!FormFields.Any() && html.ParentNode != null) //forms can have a with agility pack
            {
                nodeParser = new AgilityNodeParser(html.ParentNode);
                FormFields = ParseFormFields(nodeParser);
            }
        }

        private void ParseMethod<T>(IHtmlNodeParser<T> nodeParser)
        {
            var value = nodeParser.GetAttributeValue("method");

            if (!string.IsNullOrEmpty(value) && value.Equals("get"))
                method = HttpVerb.Get;
            else
                method = HttpVerb.Post;
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

        public WebPage Submit(Uri url, HttpVerb verb)
        {
            return browser.NavigateToPage(url, verb, SerializeFormFields());
        }

        public WebPage Submit(Uri url)
        {
            return browser.NavigateToPage(url, method, SerializeFormFields());
        }

        public WebPage Submit()
        {
            Uri url;
            if (Uri.TryCreate(Action, UriKind.Absolute, out url))
            {
                return browser.NavigateToPage(url, method, SerializeFormFields());
            }

            url = browser.Referer.Combine(action);
            return browser.NavigateToPage(url, method, SerializeFormFields());
        }

        public async Task<WebPage> SubmitAsync(Uri url, HttpVerb verb)
        {
            return await browser.NavigateToPageAsync(url, verb, SerializeFormFields());
        }

        public async Task<WebPage> SubmitAsync(Uri url)
        {
            return await browser.NavigateToPageAsync(url, method, SerializeFormFields());
        }

        public async Task<WebPage> SubmitAsync()
        {
            Uri url;
            if (Uri.TryCreate(Action, UriKind.Absolute, out url))
            {
                return await browser.NavigateToPageAsync(url, method, SerializeFormFields());
            }

            url = browser.Referer.Combine(action);
            return await browser.NavigateToPageAsync(url, method, SerializeFormFields());
        }

        public HttpVerb Method
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
