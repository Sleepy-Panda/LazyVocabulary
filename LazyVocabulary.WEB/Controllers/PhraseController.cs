using LazyVocabulary.BLL.Services;
using LazyVocabulary.WEB.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LazyVocabulary.WEB.Controllers
{
    public class PhraseController : Controller
    {
        private SourcePhraseService _sourcePhraseService;

        public PhraseController(SourcePhraseService service)
        {
            _sourcePhraseService = service;
        }

        // http://stackoverflow.com/questions/21616052/binding-arrays-in-asp-net-mvc-without-index
        // http://jsfiddle.net/716d58kw/

        // GET: Phrase
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
                .Select(p => new IndexSourcePhrasesViewModel
                {
                    Id = p.Id,
                    Value = p.Value,
                    Translations = p.TranslatedPhrases,
                    DictionaryId = p.DictionaryId,
                });

            return View(model);
        }
    }
}