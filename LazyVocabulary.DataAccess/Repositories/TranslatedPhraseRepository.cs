using LazyVocabulary.Common.Entities;
using LazyVocabulary.DataAccess.EF;
using LazyVocabulary.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LazyVocabulary.DataAccess.Repositories
{
    public class TranslatedPhraseRepository : IRepository<TranslatedPhrase>
    {
        private readonly ApplicationContext _db;

        public TranslatedPhraseRepository(ApplicationContext context)
        {
            _db = context;
        }

        public IEnumerable<TranslatedPhrase> GetAll()
        {
            return _db.TranslatedPhrases;
        }

        public TranslatedPhrase Get(int id)
        {
            return _db.TranslatedPhrases.Find(id);
        }

        public void Create(TranslatedPhrase item)
        {
            _db.TranslatedPhrases.Add(item);
        }

        public void Update(TranslatedPhrase item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            TranslatedPhrase item = _db.TranslatedPhrases.Find(id);

            if (item != null)
            {
                _db.TranslatedPhrases.Remove(item);
            }
        }

        public IEnumerable<TranslatedPhrase> Find(Func<TranslatedPhrase, bool> predicate)
        {
            return _db.TranslatedPhrases.Where(predicate);
        }
    }
}
