using LazyVocabulary.BLL.OperationDetails;
using LazyVocabulary.Common.Entities;
using LazyVocabulary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LazyVocabulary.BLL.Services
{
    public class SourcePhraseService
    {
        public IUnitOfWork _database { get; set; }

        public SourcePhraseService(IUnitOfWork database)
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
    }
}
