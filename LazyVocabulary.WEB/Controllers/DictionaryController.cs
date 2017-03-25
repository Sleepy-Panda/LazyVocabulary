using LazyVocabulary.BLL.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyVocabulary.WEB.Controllers
{
    [Authorize]
    public class DictionaryController : Controller
    {
        private DictionaryService _dictionaryService;

        public DictionaryController(DictionaryService service)
        {
            _dictionaryService = service;
        }

        // GET: Dictionary
        [HttpGet]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            var resultWithData = _dictionaryService.GetByUserId(userId);

            if (!resultWithData.Success)
            {
                //
            }

            var dictionaries = resultWithData.ResultData;

            return View();
        }
    }
}