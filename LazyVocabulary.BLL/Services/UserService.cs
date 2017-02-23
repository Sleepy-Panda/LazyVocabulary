using LazyVocabulary.BLL.DTO;
using LazyVocabulary.BLL.Identity;
using LazyVocabulary.BLL.Interfaces;
using LazyVocabulary.DAL.EF;
using LazyVocabulary.DAL.Entities;
using System.Threading.Tasks;

namespace LazyVocabulary.BLL.Services
{
    public class UserService : IUserService
    {
        private IIdentityUnitOfWork database { get; set; }

        public UserService(IIdentityUnitOfWork uow)
        {
            database = uow;
        }

        public ApplicationContext GetContext()
        {
            return database.ApplicationContext;
        }

        public ApplicationUserManager GetUserManager()
        {
            return database.UserManager;
        }

        public async Task Create(UserDTO user)
        {
            var appUser = new ApplicationUser { UserName = user.UserName, Email = user.Email };
            var result = await database.UserManager.CreateAsync(appUser, "8115269Cdtnf!");
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}