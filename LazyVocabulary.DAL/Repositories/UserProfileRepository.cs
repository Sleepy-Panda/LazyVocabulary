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
        private readonly ApplicationContext db;

        public UserProfileRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return db.UserProfiles;
        }

        public UserProfile Get(int id)
        {
            return db.UserProfiles.Find(id);
        }

        public void Create(UserProfile item)
        {
            db.UserProfiles.Add(item);
        }

        public void Update(UserProfile item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            UserProfile item = db.UserProfiles.Find(id);
            if (item != null)
            {
                db.UserProfiles.Remove(item);
            }
        }

        public IEnumerable<UserProfile> Find(Func<UserProfile, bool> predicate)
        {
            return db.UserProfiles
                .Where(predicate);
        }
    }
}
