using LazyVocabulary.Common.Entities;
using LazyVocabulary.DataAccess.Interfaces;
using LazyVocabulary.Logic.OperationDetails;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace LazyVocabulary.Logic.Services
{
    public class DictionaryService
    {
        private IUnitOfWork _database { get; set; }

        public DictionaryService(IUnitOfWork database)
        {
            _database = database;
        }

        public ResultWithData<List<Dictionary>> GetByUserId(string userId)
        {
            var resultWithData = new ResultWithData<List<Dictionary>>();

            try
            {
                resultWithData.ResultData = _database.Dictionaries
                    .Find(d => d.ApplicationUserId == userId)
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

        public ResultWithData<List<Dictionary>> GetByUserIdAndSearchPattern(string userId, string searchPattern, bool searchDescriptions)
        {
            var resultWithData = new ResultWithData<List<Dictionary>>();

            try
            {
                if (String.IsNullOrEmpty(searchPattern))
                {
                    resultWithData.ResultData = _database.Dictionaries
                    .Find(d => d.ApplicationUserId == userId)
                    .ToList();
                }
                else
                {
                    List<Dictionary> dictionaries;

                    if (searchDescriptions)
                    {
                        dictionaries = _database.Dictionaries
                            .Find(d => (d.ApplicationUserId == userId) && 
                                       (d.Name.ToLower().Contains(searchPattern.ToLower()) || (d.Description != null && d.Description.ToLower().Contains(searchPattern.ToLower()))))
                            .ToList();
                    }
                    else
                    {
                        dictionaries = _database.Dictionaries
                            .Find(d => d.ApplicationUserId == userId && d.Name.ToLower().Contains(searchPattern.ToLower()))
                            .ToList();
                    }

                    resultWithData.ResultData = dictionaries;
                }
                
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

        public ResultWithData<string> GetLanguagePairById(int dictionaryId)
        {
            var resultWithData = new ResultWithData<string>();

            try
            {
                var dictionary = _database.Dictionaries.Get(dictionaryId);
                var languagePair = $"{ dictionary.SourceLanguage.Code }-{ dictionary.TargetLanguage.Code }";

                resultWithData.ResultData = languagePair.ToLower();
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

        public ResultWithData<Dictionary> GetById(int dictionaryId, string userId)
        {
            var resultWithData = new ResultWithData<Dictionary>();

            try
            {
                var dictionary = _database.Dictionaries.Get(dictionaryId);
                resultWithData.ResultData = dictionary.ApplicationUserId == userId 
                    ? dictionary 
                    : null;
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

        public async Task<Result> Create(dynamic dictionaryFromView)
        {
            var result = new Result();

            try
            {
                var dictionary = new Dictionary
                {
                    Name = dictionaryFromView.Name,
                    Description = dictionaryFromView.Description,
                    ApplicationUserId = dictionaryFromView.ApplicationUserId,
                    SourceLanguageId = dictionaryFromView.SourceLanguageId,
                    TargetLanguageId = dictionaryFromView.TargetLanguageId,
                };
                _database.Dictionaries.Create(dictionary);
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

        public async Task<Result> Update(dynamic dictionaryFromView)
        {
            var result = new Result();

            try
            {
                var dictionary = new Dictionary
                {
                    Id = dictionaryFromView.Id,
                    Name = dictionaryFromView.Name,
                    Description = dictionaryFromView.Description,
                    ApplicationUserId = dictionaryFromView.ApplicationUserId,
                    SourceLanguageId = dictionaryFromView.SourceLanguageId,
                    TargetLanguageId = dictionaryFromView.TargetLanguageId,
                    UpdatedAt = DateTime.Now,
                };
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

        public async Task<Result> Delete(int dictionaryId)
        {
            var result = new Result();

            try
            {
                _database.Dictionaries.Delete(dictionaryId);
                await _database.SaveAsync();

                result.Success = true;
            }
            // TODO: Test
            catch (DbUpdateConcurrencyException)
            {
                result.Success = true;
            }
            catch (Exception ex)
            {
                //string exc = ex.GetType().ToString();
                result.Success = false;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
            }

            return result;
        }

        public bool IsDictionaryNameAvailable(string dictionaryName, string userId)
        {
            bool result = _database.Dictionaries
                .Find(d => d.Name.Equals(dictionaryName) && d.ApplicationUserId.Equals(userId))
                .SingleOrDefault() == null;

            return result;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _database.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
