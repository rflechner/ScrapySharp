using System;
using System.IO;
using HtmlAgilityPack;
using NUnit.Framework;
using ScrapySharp.Html;
using ScrapySharp.Html.Dom;
using ScrapySharp.Html.Forms;
using ScrapySharp.Extensions;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ScrapySharp.Network;

namespace ScrapySharp.Tests
{
    [TestFixture]
    public class When_use_web_forms
    {
        [Test,Ignore("Integration")]
        public async Task When_browsing_using_helpers()
        {
            ScrapingBrowser browser = new ScrapingBrowser();

            //set UseDefaultCookiesParser as false if a website returns invalid cookies format
            //browser.UseDefaultCookiesParser = false;

            WebPage homePage = browser.NavigateToPage(new Uri("http://www.bing.com/"));

            PageWebForm form = homePage.FindFormById("sb_form");
            form["q"] = "scrapysharp";
            form.Method = HttpMethod.Get;
            WebPage resultsPage = await form.SubmitAsync();

            HtmlNode[] resultsLinks = resultsPage.Html.CssSelect("div.sb_tlst h3 a").ToArray();

            WebPage blogPage = await resultsPage.FindLinks(By.Text("romcyber blog | Just another WordPress site")).Single().ClickAsync();
        }

        [Test]
        public void When_parsing_form()
        {
            var source = File.ReadAllText("Html/WebFormPage.htm");
            var html = HDocument.Parse(source);
            
            var webForm = new WebForm(html.CssSelect("form[name=TestForm]").Single());

            Assert.AreEqual(5, webForm.FormFields.Count);
        }

        [Test]
        public void When_parsing_form_with_agility_pack()
        {
            var source = File.ReadAllText("Html/WebFormPage.htm");
            var html = source.ToHtmlNode();

            var webForm = new WebForm(html.CssSelect("form[name=TestForm]").Single());

            //Because HtmlAgilityPack fails the form parsing !
            Assert.AreNotEqual(5, webForm.FormFields.Count);
        }

        [Test]
        public void When_parsing_partial_view()
        {
            var source = File.ReadAllText("Html/Form1.htm");
            var html = HDocument.Parse(source);

            var form = html.CssSelect("form").SingleOrDefault();

            Assert.IsNotNull(form);
        }
    }
}