using LazyVocabulary.Web.Filters;
using System.Web.Mvc;

namespace LazyVocabulary.Web.Controllers
{
    [RedirectOnException]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Culture = HttpContext.Request.Cookies["locale"]?.Value;
                return RedirectToAction("Index", "Dictionary");
            }

            return View();
        }
    }
}