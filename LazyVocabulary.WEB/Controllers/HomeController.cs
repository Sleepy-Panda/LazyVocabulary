using System.Web.Mvc;

namespace LazyVocabulary.WEB.Controllers
{
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