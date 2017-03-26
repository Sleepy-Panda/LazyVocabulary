using LazyVocabulary.BLL.DTO;
using LazyVocabulary.BLL.Interfaces;
using LazyVocabulary.BLL.OperationDetails;
using LazyVocabulary.DAL.Entities;
using LazyVocabulary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public ResultWithData<DictionaryDTO> GetById(int id)
        {
            var resultWithData = new ResultWithData<DictionaryDTO>();

            try
            {
                var item = Database.Dictionaries.Get(id);
                resultWithData.ResultData = new DictionaryDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    CreatedAt = item.CreatedAt,
                    ViewsCount = item.ViewsCount,
                    ApplicationUserId = item.ApplicationUserId,
                    SourceLanguageId = item.SourceLanguageId,
                    TargetLanguageId = item.TargetLanguageId,
                };
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

        public async Task<Result> Create(DictionaryDTO dto)
        {
            var result = new Result();

            try
            {
                var dictionary = new Dictionary
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    ApplicationUserId = dto.ApplicationUserId,
                    SourceLanguageId = dto.SourceLanguageId,
                    TargetLanguageId = dto.TargetLanguageId,
                };
                Database.Dictionaries.Create(dictionary);
                await Database.SaveAsync();

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

        public async Task<Result> Update(DictionaryDTO dto)
        {
            var result = new Result();

            try
            {
                var dictionary = new Dictionary
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    ApplicationUserId = dto.ApplicationUserId,
                    SourceLanguageId = dto.SourceLanguageId,
                    TargetLanguageId = dto.TargetLanguageId,
                };
                Database.Dictionaries.Update(dictionary);
                await Database.SaveAsync();

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

        public bool IsDictionaryNameAvailable(string dictionaryName)
        {
            bool result = Database.Dictionaries
                .Find(d => d.Name.Equals(dictionaryName))
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
