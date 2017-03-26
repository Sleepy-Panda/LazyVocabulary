using LazyVocabulary.BLL.DTO;
using LazyVocabulary.BLL.Services;
using LazyVocabulary.WEB.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: Dictionary/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Dictionary/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDictionaryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Result = "Error";
                return PartialView("_Create", model);
            }

            var dto = new DictionaryDTO
            {
                Name = model.Name,
                Description = model.Description,
                ApplicationUserId = User.Identity.GetUserId(),
                SourceLanguageId = 1,
                TargetLanguageId = 2,
            };

            var result = await _dictionaryService.Create(dto);

            if (!result.Success)
            {
                ViewBag.Result = "Error";
                return PartialView("_Create", model);
            }

            ViewBag.Result = "Success";
            return PartialView("_Create");
        }

        [HttpGet]
        public JsonResult IsDictionaryNameAvailable(string name)
        {
            bool result = _dictionaryService.IsDictionaryNameAvailable(name);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}