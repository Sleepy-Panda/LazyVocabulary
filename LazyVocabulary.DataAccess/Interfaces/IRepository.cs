using System;
using System.Collections.Generic;

namespace LazyVocabulary.DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);

        void Create(T item);
        void Update(T item);
        void Delete(int id);

        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}
