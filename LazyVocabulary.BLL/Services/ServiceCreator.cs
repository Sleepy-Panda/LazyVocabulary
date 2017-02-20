using LazyVocabulary.BLL.Identity;
using LazyVocabulary.BLL.Interfaces;

namespace LazyVocabulary.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }
    }
}
