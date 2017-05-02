﻿using LazyVocabulary.DataAccess.Interfaces;
using System;

namespace LazyVocabulary.Logic.Services
{
    public class UserProfileService
    {
        private IUnitOfWork _database { get; set; }

        public UserProfileService(IUnitOfWork database)
        {
            _database = database;
        }

        //public async Task<ResultWithData<int>> CreateDefaultProfileForUserAsync()
        //{
        //    var resultWithData = new ResultWithData<int>();

        //    try
        //    {
        //        var profile = new UserProfile
        //        {
        //            CreatedAt = DateTime.Now,
        //            UpdatedAt = DateTime.Now,
        //            PasswordUpdatedAt = DateTime.Now,
        //            AvatarImagePath = "default_avatar.png",
        //            Locale = LocaleLanguage.Ru,
        //        };

        //        _database.UserProfiles.Create(profile);
        //        await _database.SaveAsync();

        //        resultWithData.ResultData = profile.Id;
        //        resultWithData.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        resultWithData.Success = false;
        //        resultWithData.Message = ex.Message;
        //        resultWithData.StackTrace = ex.StackTrace;
        //    }

        //    return resultWithData;
        //}

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _database.Dispose();
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