using LazyVocabulary.Common.Entities;
using System;
using System.Threading.Tasks;

namespace LazyVocabulary.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Language> Languages { get; }
        IRepository<UserProfile> UserProfiles { get; }
        IRepository<Dictionary> Dictionaries { get; }
        IRepository<SourcePhrase> SourcePhrases { get; }
        IRepository<TranslatedPhrase> TranslatedPhrases { get; }

        void Save();
        Task SaveAsync();
    }
}
