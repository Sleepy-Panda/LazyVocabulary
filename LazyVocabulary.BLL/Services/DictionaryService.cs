using LazyVocabulary.BLL.DTO;
using LazyVocabulary.BLL.Interfaces;
using LazyVocabulary.BLL.OperationDetails;
using LazyVocabulary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LazyVocabulary.BLL.Services
{
    public class DictionaryService : IService
    {
        public IUnitOfWork Database { get; set; }

        public DictionaryService(IUnitOfWork database)
        {
            Database = database;
        }

        public ResultWithData<List<DictionaryDTO>> GetByUserId(string userId)
        {
            var resultWithData = new ResultWithData<List<DictionaryDTO>>();

            try
            {
                resultWithData.ResultData = Database.Dictionaries
                    .Find(d => d.ApplicationUserId == userId)
                    .Select(d => new DictionaryDTO {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        CreatedAt = d.CreatedAt,
                        ViewsCount = d.ViewsCount,
                        ApplicationUserId = d.ApplicationUserId,
                        SourceLanguageId = d.SourceLanguageId,
                        TargetLanguageId = d.TargetLanguageId,
                    })
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

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Database.Dispose();
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
