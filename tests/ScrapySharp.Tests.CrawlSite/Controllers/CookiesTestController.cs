using Microsoft.AspNetCore.Mvc;

namespace ScrapySharp.Tests.CrawlSite.Controllers
{
    public class CookiesTestController : Controller
    {
        public IActionResult IncCount()
        {
            int.TryParse(Request.Cookies["VisitsCount"], out var count);

            count++;
            Response.Cookies.Append("VisitsCount", count.ToString());
            
            return Content(count.ToString());
        }
        
        public IActionResult GetCount()
        {
            var cookie = Request.Cookies["VisitsCount"];
            
            return Content(string.IsNullOrEmpty(cookie) ? "0" : cookie);
        }
        
        
    }
}