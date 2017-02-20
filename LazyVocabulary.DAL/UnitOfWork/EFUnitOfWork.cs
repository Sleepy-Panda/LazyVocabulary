using LazyVocabulary.DAL.EF;
using LazyVocabulary.DAL.Entities;
using LazyVocabulary.DAL.Interfaces;
using LazyVocabulary.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace LazyVocabulary.DAL.UnitOfWork
{
    public class EFUnitOfWork : IEFUnitOfWork
    {
        private ApplicationContext db;

        private LanguageRepository languageRepository;
        private GuiLanguageRepository guiLanguageRepository;
        private UserProfileRepository userProfileRepository;
        private DictionaryRepository dictionaryRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
        }

        public IRepository<Language> Languages
        {
            get
            {
                if (languageRepository == null)
                {
                    languageRepository = new LanguageRepository(db);
                }
                return languageRepository;
            }
        }

        public IRepository<GuiLanguage> GuiLanguages
        {
            get
            {
                if (guiLanguageRepository == null)
                {
                    guiLanguageRepository = new GuiLanguageRepository(db);
                }
                return guiLanguageRepository;
            }
        }

        public IRepository<UserProfile> UserProfiles
        {
            get
            {
                if (userProfileRepository == null)
                {
                    userProfileRepository = new UserProfileRepository(db);
                }
                return userProfileRepository;
            }
        }

        public IRepository<Dictionary> Dictionaries
        {
            get
            {
                if (dictionaryRepository == null)
                {
                    dictionaryRepository = new DictionaryRepository(db);
                }
                return dictionaryRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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
