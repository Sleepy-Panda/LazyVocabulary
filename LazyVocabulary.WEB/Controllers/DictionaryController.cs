using LazyVocabulary.BLL.Services;
using LazyVocabulary.WEB.Models;
using Microsoft.AspNet.Identity;
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
                });

            return View(model);
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

            dynamic dictionary = new ExpandoObject();
            dictionary.Name = model.Name;
            dictionary.Description = model.Description;
            dictionary.ApplicationUserId = User.Identity.GetUserId();
            dictionary.SourceLanguageId = 1;
            dictionary.TargetLanguageId = 2;

            // TODO: 1 и 2
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
            bool result = _dictionaryService.IsDictionaryNameAvailable(name);

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

            var resultWithData = _dictionaryService.GetById(id.Value);

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

            dynamic dictionary = new ExpandoObject();
            dictionary.Id = model.Id;
            dictionary.Name = model.Name;
            dictionary.Description = model.Description;
            dictionary.ApplicationUserId = User.Identity.GetUserId();
            dictionary.SourceLanguageId = 1;
            dictionary.TargetLanguageId = 2;

            // TODO - 1, 2
            var result = await _dictionaryService.Update(dictionary);

            if (!result.Success)
            {
                ViewBag.Result = "Error";
                return PartialView("_Edit", model);
            }

            ViewBag.Result = "Success";
            return PartialView("_Edit");
        }
    }
}