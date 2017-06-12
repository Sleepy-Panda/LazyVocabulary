using LazyVocabulary.Common.Entities;
using LazyVocabulary.DataAccess.EF;
using LazyVocabulary.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LazyVocabulary.DataAccess.Repositories
{
    public class SubscriptionRepository : IRepository<Subscription>
    {
        private readonly ApplicationContext _db;

        public SubscriptionRepository(ApplicationContext context)
        {
            _db = context;
        }

        public IEnumerable<Subscription> GetAll()
        {
            return _db.Subscriptions;
        }

        public Subscription Get(int id)
        {
            return _db.Subscriptions.Find(id);
        }

        public void Create(Subscription item)
        {
            _db.Subscriptions.Add(item);
        }

        public void Update(Subscription item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Subscription item = _db.Subscriptions.Find(id);

            if (item != null)
            {
                _db.Subscriptions.Remove(item);
            }
        }

        public IEnumerable<Subscription> Find(Func<Subscription, bool> predicate)
        {
            return _db.Subscriptions.Where(predicate);
        }
    }
}
