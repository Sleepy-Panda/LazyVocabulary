using LazyVocabulary.Logic.OperationDetails;
using LazyVocabulary.Common.Entities;
using LazyVocabulary.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazyVocabulary.Logic.Services
{
    public class TranslationService
    {
        private IUnitOfWork _database { get; set; }

        public TranslationService(IUnitOfWork database)
        {
            _database = database;
        }

        public ResultWithData<List<SourcePhrase>> GetByDictionaryId(int dictionaryId)
        {
            var resultWithData = new ResultWithData<List<SourcePhrase>>();

            try
            {
                resultWithData.ResultData = _database.SourcePhrases
                    .Find(s => s.DictionaryId == dictionaryId)
                    .ToList();
                resultWithData.Success = true;
            }
            catch (Exception ex)
            {
                resultWithData.Success = false;
                resultWithData.Message = ex.Message;
                resultWithData.StackTrace = ex.StackTrace;
            }

            return resultWithData;
        }

        public async Task<Result> Create(dynamic translationFromView)
        {
            var result = new Result();

            try
            {
                var sourcePhrase = new SourcePhrase
                {
                    DictionaryId = translationFromView.DictionaryId,
                    Value = translationFromView.Value,
                };
                _database.SourcePhrases.Create(sourcePhrase);
                await _database.SaveAsync();

                foreach (var value in translationFromView.Translations)
                {
                    var translation = new TranslatedPhrase
                    {
                        SourcePhraseId = sourcePhrase.Id,
                        Value = value,
                    };
                    _database.TranslatedPhrases.Create(translation);
                }

                await _database.SaveAsync();

                var dictionary = _database.Dictionaries.Get(translationFromView.DictionaryId);
                dictionary.UpdatedAt = DateTime.Now;
                _database.Dictionaries.Update(dictionary);
                await _database.SaveAsync();
            
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
            }

            return result;
        }

        public async Task<Result> Delete(int sourcePhraseId)
        {
            var result = new Result();

            try
            {
                _database.SourcePhrases.Delete(sourcePhraseId);
                await _database.SaveAsync();

                var translations = _database.TranslatedPhrases.Find(t => t.SourcePhraseId == sourcePhraseId);

                foreach (var translation in translations) {
                    _database.TranslatedPhrases.Delete(translation.Id);
                }

                await _database.SaveAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
            }

            return result;
        }

        public async Task<Result> Copy(int sourcePhraseId, int dictionaryId)
        {
            var result = new Result();

            try
            {
                var sourcePhrase = _database.SourcePhrases.Get(sourcePhraseId);
                sourcePhrase.DictionaryId = dictionaryId;

                _database.SourcePhrases.Create(sourcePhrase);
                await _database.SaveAsync();

                var translations = _database.TranslatedPhrases.Find(t => t.SourcePhraseId == sourcePhraseId);

                foreach (var translation in translations)
                {
                    translation.SourcePhraseId = sourcePhrase.Id;
                    _database.TranslatedPhrases.Create(translation);
                }

                await _database.SaveAsync();

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
            }

            return result;
        }
    }
}
