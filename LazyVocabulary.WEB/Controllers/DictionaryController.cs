using LazyVocabulary.BLL.Services;
using LazyVocabulary.WEB.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LazyVocabulary.WEB.Controllers
{
    [Authorize]
    public class DictionaryController : Controller
    {
        private DictionaryService _dictionaryService;
        private LanguageService _languageService;

        public DictionaryController(DictionaryService dService, LanguageService lService)
        {
            _dictionaryService = dService;
            _languageService = lService;
        }

        // GET: Dictionary
        [HttpGet]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            var resultWithData = _dictionaryService.GetByUserId(userId);

            if (!resultWithData.Success)
            {
                // TODO
            }

            var dictionaries = resultWithData.ResultData;

            var model = dictionaries
                .Select(d => new IndexDictionaryViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    SourceLanguageImagePath = d.SourceLanguage.FlagImagePath,
                    TargetLanguageImagePath = d.TargetLanguage.FlagImagePath,
                    PhrasesCount = d.SourcePhrases.Count,
                    CreatedAt = d.CreatedAt,
                });

            return View("Index", model);
        }

        // GET: Dictionary/Create
        [HttpGet]
        public ActionResult Create()
        {
            var resultWithData = _languageService.GetAll();

            if (!resultWithData.Success)
            {
                // TODO
            }

            var languages = resultWithData.ResultData;
            ViewBag.SourceLanguageId = ViewBag.TargetLanguageId = new SelectList(languages, "Id", "Name");

            return View("Create");
        }

        // POST: Dictionary/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDictionaryViewModel model)
        {
            var resultWithData = _languageService.GetAll();

            if (!resultWithData.Success)
            {
                // TODO
            }

            var languages = resultWithData.ResultData;
            ViewBag.SourceLanguageId = ViewBag.TargetLanguageId = new SelectList(languages, "Id", "Name");

            if (!ModelState.IsValid)
            {
                ViewBag.Result = "Error";
                return PartialView("_Create", model);
            }

            dynamic dictionary = new ExpandoObject();
            dictionary.Name = model.Name;
            dictionary.Description = model.Description;
            dictionary.ApplicationUserId = User.Identity.GetUserId();
            dictionary.SourceLanguageId = model.SourceLanguageId;
            dictionary.TargetLanguageId = model.TargetLanguageId;

            var result = await _dictionaryService.Create(dictionary);

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
            string userId = User.Identity.GetUserId();
            bool result = _dictionaryService.IsDictionaryNameAvailable(name, userId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Dictionary/Edit/1
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.GetUserId();

            var resultWithData = _dictionaryService.GetById(id.Value, userId);

            if (!resultWithData.Success)
            {
                // TODO
            }

            var dictionary = resultWithData.ResultData;

            if (dictionary == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var model = new EditDictionaryViewModel
            {
                Id = dictionary.Id,
                Name = dictionary.Name,
                Description = dictionary.Description,
            };

            var resultWithDataLanguages = _languageService.GetAll();

            if (!resultWithDataLanguages.Success)
            {
                // TODO
            }

            var languages = resultWithDataLanguages.ResultData;
            ViewBag.SourceLanguageId = new SelectList(languages, "Id", "Name", dictionary.SourceLanguageId);
            ViewBag.TargetLanguageId = new SelectList(languages, "Id", "Name", dictionary.TargetLanguageId);

            return View("Edit", model);
        }

        // POST: Dictionary/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditDictionaryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Result = "Error";
                return PartialView("_Edit", model);
            }

            var resultWithDataLanguages = _languageService.GetAll();

            if (!resultWithDataLanguages.Success)
            {
                // TODO
            }

            var languages = resultWithDataLanguages.ResultData;
            ViewBag.SourceLanguageId = new SelectList(languages, "Id", "Name", model.SourceLanguageId);
            ViewBag.TargetLanguageId = new SelectList(languages, "Id", "Name", model.TargetLanguageId);

            // TODO: доступно ли название 

            dynamic dictionary = new ExpandoObject();
            dictionary.Id = model.Id;
            dictionary.Name = model.Name;
            dictionary.Description = model.Description;
            dictionary.ApplicationUserId = User.Identity.GetUserId();
            dictionary.SourceLanguageId = model.SourceLanguageId;
            dictionary.TargetLanguageId = model.TargetLanguageId;

            var result = await _dictionaryService.Update(dictionary);

            if (!result.Success)
            {
                ViewBag.Result = "Error";
                return PartialView("_Edit", model);
            }

            ViewBag.Result = "Success";
            return PartialView("_Edit");
        }

        // GET: Dictionary/Details/1
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.GetUserId();

            var resultWithData = _dictionaryService.GetById(id.Value, userId);

            if (!resultWithData.Success)
            {
                // TODO
            }

            var dictionary = resultWithData.ResultData;

            if (dictionary == null)
            {
                return HttpNotFound();
            }

            var model = new DetailsDictionaryViewModel
            {
                Id = dictionary.Id,
                Name = dictionary.Name,
                Description = dictionary.Description,
                SourceLanguageImagePath = dictionary.SourceLanguage.FlagImagePath,
                TargetLanguageImagePath = dictionary.TargetLanguage.FlagImagePath,
                SourcePhrases = dictionary.SourcePhrases,
                CreatedAt = dictionary.CreatedAt,
            };

            return View(model);
        }

        [HttpGet]
        public JsonResult GetLanguagePair(int dictionaryId)
        {
            string languagePair = String.Empty;
            var resultWithData = _dictionaryService.GetLanguagePairById(dictionaryId);

            if (resultWithData.Success)
            {
                languagePair = resultWithData.ResultData;
            }

            return Json(new { languagePair = languagePair }, JsonRequestBehavior.AllowGet);
        }
    }
}