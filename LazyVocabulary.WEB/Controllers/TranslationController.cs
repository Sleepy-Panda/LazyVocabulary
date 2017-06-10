using LazyVocabulary.Common.Entities;
using LazyVocabulary.Logic.Services;
using LazyVocabulary.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LazyVocabulary.Web.Controllers
{
    [Authorize]
    public class TranslationController : Controller
    {
        private TranslationService _translationService;
        private DictionaryService _dictionaryService;

        public TranslationController(TranslationService tService, DictionaryService dService)
        {
            _translationService = tService;
            _dictionaryService = dService;
        }

        // GET: Translation/1
        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var resultWithData = _translationService.GetByDictionaryId(id.Value);

            if (!resultWithData.Success)
            {
                // TODO
            }

            var phrases = resultWithData.ResultData;

            var model = phrases
                .OrderByDescending(p => p.Id)
                .Select(p => new IndexTranslationViewModel
                {
                    Id = p.Id,
                    Value = p.Value,
                    Translations = p.TranslatedPhrases,
                    DictionaryId = p.DictionaryId,
                });

            ViewBag.DictionaryId = id;

            string userId = User.Identity.GetUserId();
            var dictionariesExcludingCurrent = GetDictionaries(userId)
                .Where(d => d.Id != id.Value)
                .Select(d => new {
                    Id = d.Id,
                    Name = $"{ d.Name } ({ d.SourceLanguage.Code }-{ d.TargetLanguage.Code })",
                    Group = "Мои словари",
                })
                .ToList();

            ViewBag.Dictionaries = new SelectList(dictionariesExcludingCurrent, "Id", "Name");
            ViewBag.DictionariesCount = dictionariesExcludingCurrent.Count;

            return View("Index", model);
        }

        // GET: Translation/Search
        [HttpGet]
        public ActionResult Search(int? id, string searchPattern = null)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            searchPattern = searchPattern == null
                ? String.Empty
                : searchPattern.Trim();

            var resultWithData = _translationService.GetByDictionaryIdAndSearchPattern(id.Value, searchPattern);

            if (!resultWithData.Success)
            {
                return PartialView("_List", new List<IndexTranslationViewModel>());
            }

            var phrases = resultWithData.ResultData;

            var model = phrases
                .Select(p => new IndexTranslationViewModel
                {
                    Id = p.Id,
                    Value = p.Value,
                    Translations = p.TranslatedPhrases,
                    DictionaryId = p.DictionaryId,
                });

            ViewBag.DictionaryId = id;

            return PartialView("_List", model);
        }

        // GET: Translation/Create
        [HttpGet]
        public ActionResult Create(int? id)
        {
            string userId = User.Identity.GetUserId();
            var dictionaries = GetDictionaries(userId)
                .Select(d => new {
                    Id = d.Id,
                    Name = $"{ d.Name } ({ d.SourceLanguage.Code }-{ d.TargetLanguage.Code })",
                    Group = "Мои словари",
                })
                .ToList();

            if (id.HasValue)
            {
                ViewBag.DictionaryId = new SelectList(dictionaries, "Id", "Name", "Group", id.Value);
            }
            else
            {
                ViewBag.DictionaryId = new SelectList(dictionaries, "Id", "Name");
            }

            return View("Create");
        }

        // POST: Translation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTranslationViewModel model, string[] translations)
        {
            string userId = User.Identity.GetUserId();
            var dictionaries = GetDictionaries(userId)                
                .Select(d => new {
                    Id = d.Id,
                    Name = $"{ d.Name } ({ d.SourceLanguage.Code }-{ d.TargetLanguage.Code })",
                    Group = "Мои словари",
                })
                .ToList();
            ViewBag.DictionaryId = new SelectList(dictionaries, "Id", "Name", "Group", model.DictionaryId);

            if (!ModelState.IsValid)
            {
                ViewBag.Result = "Error";
                return PartialView("_Create", model);
            }

            var validTranslations = translations
                .Where(t => !String.IsNullOrEmpty(t))
                .ToList();

            dynamic translation = new ExpandoObject();
            translation.DictionaryId = model.DictionaryId;
            translation.Value = model.Value;
            translation.Translations = validTranslations;

            var result = await _translationService.CreateAsync(translation);

            if (!result.Success)
            {
                ViewBag.Result = "Error";
                return PartialView("_Create", model);
            }

            ViewBag.Result = "Success";
            return PartialView("_Create");
        }

        // POST: Translation/Delete/1
        [HttpPost]
        public async Task<JsonResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            var result = await _translationService.DeleteAsync(id.Value);

            if (!result.Success)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // POST: Translation/Copy?translationId=1&dictionaryId=1
        [HttpPost]
        public async Task<JsonResult> Copy(int? translationId, int? dictionaryId)
        {
            if (!translationId.HasValue && !dictionaryId.HasValue)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            var result = await _translationService.CopyAsync(translationId.Value, dictionaryId.Value);

            if (!result.Success)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Dictionary> GetDictionaries(string userId)
        {
            var resultWithData = _dictionaryService.GetByUserId(userId);

            if (!resultWithData.Success)
            {
                return new List<Dictionary>();
            }

            return resultWithData.ResultData.OrderBy(d => d.Name);
        }
    }
}