# Getting started

__ScrapySharp__ has a Web Client able to simulate a real Web browser (handle referrer, cookies …)

Html parsing has to be as natural as possible. So I like to use CSS Selectors and Linq.

This framework wraps HtmlAgilityPack.

__ScrapySharp__ is not abble to emulate `javascript`, so scraping dynamic rendered web page is not easy as using selenium.


## Basic examples of CssSelect usages

```C#

using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

class Example
{
    public void Main()
    {
        var divs = html.CssSelect("div");  //all div elements
        var nodes = html.CssSelect("div.content"); //all div elements with css class ‘content’
        var nodes = html.CssSelect("div.widget.monthlist"); //all div elements with the both css class
        var nodes = html.CssSelect("#postPaging"); //all HTML elements with the id postPaging
        var nodes = html.CssSelect("div#postPaging.testClass"); // all HTML elements with the id postPaging and css class testClass

        var nodes = html.CssSelect("div.content > p.para"); //p elements who are direct children of div elements with css class ‘content’

        var nodes = html.CssSelect("input[type=text].login"); // textbox with css class login
    }
}
```

## Scrapysharp can also simulate a web browser

```C#

var http = new HttpClient();
var browser = new ModernScrapingBrowser("your-bot-name");

WebPage homePage = await browser.NavigateToPageAsync(new Uri("https://www.nuget.org/"));

// we find the form using a CSS selector
var form = homePage.FindFormByCssSelector("div.container form");
// input text is in texbox named "q"
form["q"] = "scrapysharp";
// we submit the form
WebPage resultsPage = await form.SubmitAsync();

// we parse search results
HtmlNode[] resultsLinks = resultsPage.Html.CssSelect("div.package-header a.package-title").ToArray();
var packagesNames = resultsLinks.Select(l => l.InnerText).ToArray();

```

## Install Scrapysharp in your project

It's easy to use Scrapysharp in your project.

A Nuget package exists on [nuget.org](https://www.nuget.org/packages/ScrapySharp)

