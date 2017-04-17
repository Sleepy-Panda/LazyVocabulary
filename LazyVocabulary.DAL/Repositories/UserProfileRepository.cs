using LazyVocabulary.Common.Entities;
using LazyVocabulary.DAL.EF;
using LazyVocabulary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LazyVocabulary.DAL.Repositories
{
    public class UserProfileRepository : IRepository<UserProfile>
    {
        private readonly ApplicationContext _db;

        public UserProfileRepository(ApplicationContext context)
        {
            _db = context;
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return _db.UserProfiles;
        }

        public UserProfile Get(int id)
        {
            return _db.UserProfiles.Find(id);
        }

        public void Create(UserProfile item)
        {
            _db.UserProfiles.Add(item);
        }

        public void Update(UserProfile item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            UserProfile item = _db.UserProfiles.Find(id);
            if (item != null)
            {
                _db.UserProfiles.Remove(item);
            }
        }

        public IEnumerable<UserProfile> Find(Func<UserProfile, bool> predicate)
        {
            return _db.UserProfiles
                .Where(predicate);
        }
    }
}
