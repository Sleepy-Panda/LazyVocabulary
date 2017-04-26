using LazyVocabulary.BLL.OperationDetails;
using LazyVocabulary.Common.Entities;
using LazyVocabulary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazyVocabulary.BLL.Services
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

        public ResultWithData<Dictionary> GetById(int id, string userId)
        {
            var resultWithData = new ResultWithData<Dictionary>();

            try
            {
                var dictionary = _database.Dictionaries.Get(id);
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
