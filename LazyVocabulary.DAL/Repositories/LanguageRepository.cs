using LazyVocabulary.Common.Entities;
using LazyVocabulary.DAL.EF;
using LazyVocabulary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LazyVocabulary.DAL.Repositories
{
    public class LanguageRepository : IRepository<Language>
    {
        private readonly ApplicationContext db;

        public LanguageRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Language> GetAll()
        {
            return db.Languages;
        }

        public Language Get(int id)
        {
            return db.Languages.Find(id);
        }

        public void Create(Language item)
        {
            db.Languages.Add(item);
        }

        public void Update(Language item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Language item = db.Languages.Find(id);
            if (item != null)
            {
                db.Languages.Remove(item);
            }
        }

        public IEnumerable<Language> Find(Func<Language, bool> predicate)
        {
            return db.Languages
                .Where(predicate);
        }
    }
}
