using LazyVocabulary.Common.Entities;
using System;
using System.Threading.Tasks;

namespace LazyVocabulary.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Language> Languages { get; }
        IRepository<GuiLanguage> GuiLanguages { get; }
        IRepository<UserProfile> UserProfiles { get; }
        IRepository<Dictionary> Dictionaries { get; }

        void Save();
        Task SaveAsync();
    }
}
