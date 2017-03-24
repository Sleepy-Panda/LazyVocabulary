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
                return View("Test");
            }

            return View();
        }
    }
}