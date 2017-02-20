using LazyVocabulary.DAL.EF;
using LazyVocabulary.DAL.Entities;
using LazyVocabulary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LazyVocabulary.DAL.Repositories
{
    public class GuiLanguageRepository : IRepository<GuiLanguage>
    {
        private readonly ApplicationContext db;

        public GuiLanguageRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<GuiLanguage> GetAll()
        {
            return db.GuiLanguages;
        }

        public GuiLanguage Get(int id)
        {
            return db.GuiLanguages.Find(id);
        }

        public void Create(GuiLanguage item)
        {
            db.GuiLanguages.Add(item);
        }

        public void Update(GuiLanguage item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            GuiLanguage item = db.GuiLanguages.Find(id);
            if (item != null)
            {
                db.GuiLanguages.Remove(item);
            }
        }

        public IEnumerable<GuiLanguage> Find(Func<GuiLanguage, bool> predicate)
        {
            return db.GuiLanguages
                .Where(predicate);
        }
    }
}
