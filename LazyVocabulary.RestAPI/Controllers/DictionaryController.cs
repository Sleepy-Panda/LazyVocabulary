using LazyVocabulary.DataAccess.UnitOfWork;
using LazyVocabulary.Logic.Helpers;
using LazyVocabulary.Logic.Services;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LazyVocabulary.RestAPI.Controllers
{
    public class DictionaryController : ApiController
    {
        private UserService _userService;
        private TranslationService _translationService;
        private DictionaryService _dictionaryService;
        private LanguageService _languageService;

        public UserService UserService
        {
            get
            {
                return _userService ?? HttpContext.Current.GetOwinContext().Get<UserService>();
            }
            private set
            {
                _userService = value;
            }
        }

        public DictionaryController()
        {
            _translationService = new TranslationService(new UnitOfWork("DefaultConnection"));
            _dictionaryService = new DictionaryService(new UnitOfWork("DefaultConnection"));
            _languageService = new LanguageService(new UnitOfWork("DefaultConnection"));
        }

        [HttpGet]
        public async Task<IHttpActionResult> AddToDictionary(string token, string value, [FromUri] string[] translations)
        {
            var resultWithData = await UserService.GetUserIdByTokenAsync(token);

            if (!resultWithData.Success)
            {
                return BadRequest();
            }

            var userId = resultWithData.ResultData;

            if (String.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            var resultWithDataSourceLanguage = _languageService.GetIdByCode("en");
            var resultWithDataTargetLanguage = _languageService.GetIdByCode("ru");

            if (!resultWithDataSourceLanguage.Success || !resultWithDataTargetLanguage.Success)
            {
                return InternalServerError();
            }

            // Create dictionary if not exists.
            var resultWithDataDictionary = await _dictionaryService.CreateForPluginAsync(
                userId, 
                ConfigurationHelper.DictionaryNameForPluginRuEn,
                ConfigurationHelper.DictionaryDescriptionForPluginRuEn,
                resultWithDataSourceLanguage.ResultData,
                resultWithDataTargetLanguage.ResultData
            );

            if (!resultWithDataDictionary.Success)
            {
                return InternalServerError();
            }

            translations = translations ?? new string[0];

            var validTranslations = translations
                .Where(t => !String.IsNullOrEmpty(t))
                .ToList();

            dynamic translation = new ExpandoObject();
            translation.DictionaryId = resultWithDataDictionary.ResultData;
            translation.Value = value;
            translation.Translations = validTranslations;

            var result = await _translationService.CreateAsync(translation);

            if (!result.Success)
            {
                return InternalServerError();
            }

            return Ok(new { success = true });
        }
    }
}