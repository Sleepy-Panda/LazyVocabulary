using LazyVocabulary.DAL.Interfaces;
using System;

namespace LazyVocabulary.BLL.Interfaces
{
    public interface IService : IDisposable
    {
        IUnitOfWork Database { get; set; }
    }
}
