using LazyVocabulary.Common.Entities;
using LazyVocabulary.DAL.EF;
using LazyVocabulary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LazyVocabulary.DAL.Repositories
{
    public class DictionaryRepository : IRepository<Dictionary>
    {
        private readonly ApplicationContext db;

        public DictionaryRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Dictionary> GetAll()
        {
            return db.Dictionaries;
        }

        public Dictionary Get(int id)
        {
            return db.Dictionaries.Find(id);
        }

        public void Create(Dictionary item)
        {
            db.Dictionaries.Add(item);
        }

        public void Update(Dictionary item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Dictionary item = db.Dictionaries.Find(id);
            if (item != null)
            {
                db.Dictionaries.Remove(item);
            }
        }

        public IEnumerable<Dictionary> Find(Func<Dictionary, bool> predicate)
        {
            return db.Dictionaries
                .Where(predicate);
        }
    }
}
