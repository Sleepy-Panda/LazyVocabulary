using LazyVocabulary.BLL.DTO;
using LazyVocabulary.BLL.Identity;
using LazyVocabulary.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyVocabulary.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        //ApplicationContext GetContext();
        ApplicationUserManager GetUserManager();

        Task Create(UserDTO userDto);
    }
}
