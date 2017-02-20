using System.Web.Mvc;

namespace LazyVocabulary.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}