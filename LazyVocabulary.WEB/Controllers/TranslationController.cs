using LazyVocabulary.BLL.Services;
using LazyVocabulary.WEB.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LazyVocabulary.WEB.Controllers
{
    [Authorize]
    public class TranslationController : Controller
    {
        private SourcePhraseService _sourcePhraseService;
        private DictionaryService _dictionaryService;

        public TranslationController(SourcePhraseService spService, DictionaryService dService)
        {
            _sourcePhraseService = spService;
            _dictionaryService = dService;
        }

        // http://stackoverflow.com/questions/21616052/binding-arrays-in-asp-net-mvc-without-index
        // http://jsfiddle.net/716d58kw/

        // GET: Phrase/1
        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.GetUserId();
            var resultWithData = _sourcePhraseService.GetByDictionaryId(id.Value);

            if (!resultWithData.Success)
            {
                // TODO
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

            return View("Index", model);
        }

        // GET: Phrase/Create
        [HttpGet]
        public ActionResult Create(int? id)
        {
            string userId = User.Identity.GetUserId();
            var resultWithData = _dictionaryService.GetByUserId(userId);

            if (!resultWithData.Success)
            {
                // TODO
            }

            var dictionaries = resultWithData.ResultData;

            if (id.HasValue)
            {
                ViewBag.DictionaryId = new SelectList(dictionaries, "Id", "Name", id.Value);
            }
            else
            {
                ViewBag.DictionaryId = new SelectList(dictionaries, "Id", "Name");
            }

            return View("Create");
        }
    }
}