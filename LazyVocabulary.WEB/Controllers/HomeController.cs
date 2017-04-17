using LazyVocabulary.WEB.Filters;
using System.Web.Mvc;

namespace LazyVocabulary.WEB.Controllers
{
    [RedirectOnException]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Culture = HttpContext.Request.Cookies["locale"].Value;
                return View("Test");
            }

            return View();
        }
    }
}