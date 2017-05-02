using LazyVocabulary.Common.Entities;
using LazyVocabulary.DataAccess.EF;
using LazyVocabulary.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LazyVocabulary.DataAccess.Repositories
{
    public class DictionaryRepository : IRepository<Dictionary>
    {
        private readonly ApplicationContext _db;

        public DictionaryRepository(ApplicationContext context)
        {
            _db = context;
        }

        public IEnumerable<Dictionary> GetAll()
        {
            return _db.Dictionaries;
        }

        public Dictionary Get(int id)
        {
            return _db.Dictionaries.Find(id);
        }

        public void Create(Dictionary item)
        {
            _db.Dictionaries.Add(item);
        }

        public void Update(Dictionary item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Dictionary item = _db.Dictionaries.Find(id);

            if (item != null)
            {
                _db.Dictionaries.Remove(item);
            }
        }

        public IEnumerable<Dictionary> Find(Func<Dictionary, bool> predicate)
        {
            return _db.Dictionaries.Where(predicate);
        }
    }
}
