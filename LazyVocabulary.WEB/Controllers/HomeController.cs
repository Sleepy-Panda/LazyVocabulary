using LazyVocabulary.Logic.Helpers;
using LazyVocabulary.Web.Filters;
using LazyVocabulary.Web.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LazyVocabulary.Web.Controllers
{
    [AllowAnonymous]
    [SetCulture]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Culture = HttpContext.Request.Cookies["locale"]?.Value;
                return RedirectToAction("Index", "Dictionary");
            }

            return View();
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ContactUs(ContactUsViewModel model)
        {
            await EmailHelper.SendEmailFromAsync(model.Email, model.Subject, model.Text);

            return RedirectToAction("ThanksForFeedback");
        }

        [HttpGet]
        public ActionResult ThanksForFeedback()
        {
            return View();
        }
    }
}