﻿using LazyVocabulary.Common.Entities;
using LazyVocabulary.DAL.EF;
using LazyVocabulary.DAL.Interfaces;
using LazyVocabulary.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace LazyVocabulary.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext _db;

        private LanguageRepository _languageRepository;
        private GuiLanguageRepository _guiLanguageRepository;
        private UserProfileRepository _userProfileRepository;
        private DictionaryRepository _dictionaryRepository;

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

        public IRepository<GuiLanguage> GuiLanguages
        {
            get
            {
                if (_guiLanguageRepository == null)
                {
                    _guiLanguageRepository = new GuiLanguageRepository(_db);
                }

                return _guiLanguageRepository;
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

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

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
    }
}
