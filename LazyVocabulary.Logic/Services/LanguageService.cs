using LazyVocabulary.Logic.OperationDetails;
using LazyVocabulary.Common.Entities;
using LazyVocabulary.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyVocabulary.Logic.Services
{
    public class LanguageService
    {
        private IUnitOfWork _database { get; set; }

        public LanguageService(IUnitOfWork database)
        {
            _database = database;
        }

        public ResultWithData<List<Language>> GetAll()
        {
            var resultWithData = new ResultWithData<List<Language>>();

            try
            {
                resultWithData.ResultData = _database.Languages
                    .GetAll()
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
    }
}
