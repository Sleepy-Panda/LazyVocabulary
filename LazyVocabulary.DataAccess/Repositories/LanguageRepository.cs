using LazyVocabulary.Common.Entities;
using LazyVocabulary.DataAccess.EF;
using LazyVocabulary.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LazyVocabulary.DataAccess.Repositories
{
    public class LanguageRepository : IRepository<Language>
    {
        private readonly ApplicationContext _db;

        public LanguageRepository(ApplicationContext context)
        {
            _db = context;
        }

        public IEnumerable<Language> GetAll()
        {
            return _db.Languages;
        }

        public Language Get(int id)
        {
            return _db.Languages.Find(id);
        }

        public void Create(Language item)
        {
            _db.Languages.Add(item);
        }

        public void Update(Language item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Language item = _db.Languages.Find(id);

            if (item != null)
            {
                _db.Languages.Remove(item);
            }
        }

        public IEnumerable<Language> Find(Func<Language, bool> predicate)
        {
            return _db.Languages.Where(predicate);
        }
    }
}
