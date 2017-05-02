using LazyVocabulary.Common.Entities;
using LazyVocabulary.DataAccess.EF;
using LazyVocabulary.DataAccess.Interfaces;
using LazyVocabulary.DataAccess.Repositories;
using System;
using System.Threading.Tasks;

namespace LazyVocabulary.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _db;

        private LanguageRepository _languageRepository;
        private UserProfileRepository _userProfileRepository;
        private DictionaryRepository _dictionaryRepository;
        private SourcePhraseRepository _sourcePhraseRepository;
        private TranslatedPhraseRepository _translatedPhraseRepository;

        public UnitOfWork(string connectionString)
        {
            _db = new ApplicationContext(connectionString);
        }

        public IRepository<Language> Languages
        {
            get
            {
                if (_languageRepository == null)
                {
                    _languageRepository = new LanguageRepository(_db);
                }

                return _languageRepository;
            }
        }

        public IRepository<UserProfile> UserProfiles
        {
            get
            {
                if (_userProfileRepository == null)
                {
                    _userProfileRepository = new UserProfileRepository(_db);
                }

                return _userProfileRepository;
            }
        }

        public IRepository<Dictionary> Dictionaries
        {
            get
            {
                if (_dictionaryRepository == null)
                {
                    _dictionaryRepository = new DictionaryRepository(_db);
                }

                return _dictionaryRepository;
            }
        }

        public IRepository<SourcePhrase> SourcePhrases
        {
            get
            {
                if (_sourcePhraseRepository == null)
                {
                    _sourcePhraseRepository = new SourcePhraseRepository(_db);
                }

                return _sourcePhraseRepository;
            }
        }

        public IRepository<TranslatedPhrase> TranslatedPhrases
        {
            get
            {
                if (_translatedPhraseRepository == null)
                {
                    _translatedPhraseRepository = new TranslatedPhraseRepository(_db);
                }

                return _translatedPhraseRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        #region Dispose
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
