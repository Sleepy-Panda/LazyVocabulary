using LazyVocabulary.BLL.Identity;
using LazyVocabulary.DAL.EF;
using System;
using System.Threading.Tasks;

namespace LazyVocabulary.BLL.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }
        ApplicationContext ApplicationContext { get; }

        void Save();
        Task SaveAsync();
    }
}
