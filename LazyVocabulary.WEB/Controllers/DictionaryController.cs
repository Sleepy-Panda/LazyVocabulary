using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyVocabulary.WEB.Controllers
{
    public class DictionaryController : Controller
    {
        // GET: Dictionary
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}