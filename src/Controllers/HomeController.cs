using Microsoft.AspNetCore.Mvc;
using System;
using Book_Store.Models.DataLayer;
using Book_Store.Models.DataLayer.Repositories;
using Book_Store.Models.DomainModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace Book_Store.Controllers
{
    public class HomeController : Controller
    {
        private Repository<Book> data { get; set; }
        public HomeController(BookstoreContext ctx) => data = new Repository<Book>(ctx);

        public IActionResult Index()
        {
            //get random book
            var random = data.Get(new QueryOptions<Book>
            {
                OrderBy = b => Guid.NewGuid()
            });

            return View(random);
        }

        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, 
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), 
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });

            return LocalRedirect(returnUrl);
        }

        public ContentResult Register()
        {
            return Content("Registration is a TO DO item");
        }
    }
}
