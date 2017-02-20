using LazyVocabulary.BLL.DTO;
using LazyVocabulary.BLL.Interfaces;
using LazyVocabulary.DAL.Entities;

namespace LazyVocabulary.BLL.Services
{
    public class UserService : IUserService
    {
        private IIdentityUnitOfWork Database { get; set; }

        public UserService(IIdentityUnitOfWork uow)
        {
            Database = uow;
        }

        public async void Create(UserDTO user)
        {
            var appUser = new ApplicationUser { UserName = user.UserName, Email = user.Email };
            var result = await Database.UserManager.CreateAsync(appUser, "8115269Cdtnf!");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
