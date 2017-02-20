using LazyVocabulary.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyVocabulary.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        void Create(UserDTO userDto);
    }
}
