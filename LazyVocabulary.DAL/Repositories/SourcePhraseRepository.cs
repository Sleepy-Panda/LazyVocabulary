using LazyVocabulary.Common.Entities;
using LazyVocabulary.DAL.EF;
using LazyVocabulary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyVocabulary.DAL.Repositories
{
    public class SourcePhraseRepository : IRepository<SourcePhrase>
    {
        private readonly ApplicationContext _db;

        public SourcePhraseRepository(ApplicationContext context)
        {
            _db = context;
        }

        public IEnumerable<SourcePhrase> GetAll()
        {
            return _db.Dictionaries;
        }

        public SourcePhrase Get(int id)
        {
            return _db.Dictionaries.Find(id);
        }

        public void Create(SourcePhrase item)
        {
            _db.Dictionaries.Add(item);
        }

        public void Update(SourcePhrase item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            SourcePhrase item = _db.Dictionaries.Find(id);
            if (item != null)
            {
                _db.Dictionaries.Remove(item);
            }
        }

        public IEnumerable<SourcePhrase> Find(Func<SourcePhrase, bool> predicate)
        {
            return _db.Dictionaries
                .Where(predicate);
        }
    }
}
