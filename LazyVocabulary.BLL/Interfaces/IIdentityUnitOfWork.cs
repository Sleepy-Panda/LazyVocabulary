using LazyVocabulary.BLL.Identity;
using System;
using System.Threading.Tasks;

namespace LazyVocabulary.BLL.Interfaces
{
    public interface IIdentityUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }

        void Save();
        Task SaveAsync();
    }
}
