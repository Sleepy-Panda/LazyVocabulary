using LazyVocabulary.Common.Entities;
using LazyVocabulary.DAL.EF;
using LazyVocabulary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LazyVocabulary.DAL.Repositories
{
    public class GuiLanguageRepository : IRepository<GuiLanguage>
    {
        private readonly ApplicationContext _db;

        public GuiLanguageRepository(ApplicationContext context)
        {
            _db = context;
        }

        public IEnumerable<GuiLanguage> GetAll()
        {
            return _db.GuiLanguages;
        }

        public GuiLanguage Get(int id)
        {
            return _db.GuiLanguages.Find(id);
        }

        public void Create(GuiLanguage item)
        {
            _db.GuiLanguages.Add(item);
        }

        public void Update(GuiLanguage item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            GuiLanguage item = _db.GuiLanguages.Find(id);
            if (item != null)
            {
                _db.GuiLanguages.Remove(item);
            }
        }

        public IEnumerable<GuiLanguage> Find(Func<GuiLanguage, bool> predicate)
        {
            return _db.GuiLanguages
                .Where(predicate);
        }
    }
}
