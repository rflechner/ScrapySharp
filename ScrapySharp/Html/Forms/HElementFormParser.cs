using System.Collections.Generic;
using System.Linq;
using ScrapySharp.Extensions;
using ScrapySharp.Html.Dom;

namespace ScrapySharp.Html.Forms
{
    public class HElementFormParser
    {
        public static List<FormField> ParseFormFields(HElement html)
        {
            var hidden = from input in html.CssSelect("input")
                         let value = input.GetAttributeValue("value", string.Empty)
                         select new FormField
                             {
                                 Name = input.GetAttributeValue("name", string.Empty),
                                 Value = string.IsNullOrEmpty(value) ? input.InnerText : value
                             };

            var selects = from @select in html.CssSelect("select")
                          let name = @select.GetAttributeValue("name", string.Empty)
                          let option =
                              @select.CssSelect("option").FirstOrDefault(o => o.Attributes["selected"] != null) ??
                              @select.CssSelect("option").FirstOrDefault()
                          let value = option.GetAttributeValue("value", string.Empty)
                          select new FormField
                              {
                                  Name = name,
                                  Value = string.IsNullOrEmpty(value) ? option.InnerText : value
                              };

            return hidden.Concat(selects).ToList();
        }
    }
}