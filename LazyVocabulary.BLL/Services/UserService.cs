using LazyVocabulary.BLL.Identity;
using LazyVocabulary.BLL.OperationDetails;
using LazyVocabulary.Common.Entities;
using LazyVocabulary.DAL.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LazyVocabulary.BLL.Services
{
    public class UserService : IDisposable
    {
        private ApplicationContext _context;

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public UserService(string connectionString)
        {
            _context = new ApplicationContext(connectionString);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
                }

                return _userManager;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                {
                    _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
                }

                return _roleManager;
            }
        }

        public ApplicationUserManager GetUserManager()
        {
            return UserManager;
        }

        public async Task<ResultWithData<string>> CreateUserAsync(dynamic userFromView)
        {
            var resultWithData = new ResultWithData<string>();

            try
            {
                var appUser = new ApplicationUser
                {
                    UserName = userFromView.UserName,
                    Email = userFromView.Email,
                    UserProfile = new UserProfile(),
                };

                var identityResult = await UserManager.CreateAsync(appUser, userFromView.Password);

                if (!identityResult.Succeeded)
                {
                    var errors = String.Empty;

                    foreach (var error in identityResult.Errors)
                    {
                        errors += $"{ error }{ Environment.NewLine }";
                    }

                    throw new Exception(errors);
                }

                resultWithData.ResultData = appUser.Id;
                resultWithData.Success = true;
            }
            catch (Exception ex)
            {
                resultWithData.Success = false;
                resultWithData.Message = ex.Message;
                resultWithData.StackTrace = ex.StackTrace;
            }

            return resultWithData;
        }

        public async Task<ResultWithData<ClaimsIdentity>> CreateIdentityAsync(dynamic userFromView)
        {
            var result = new ResultWithData<ClaimsIdentity>();

            try
            {
                ApplicationUser appUser;
                var appUserByEmail = await UserManager.FindByEmailAsync(userFromView.Email);

                if (appUserByEmail != null)
                {
                    appUser = await UserManager.FindAsync(appUserByEmail.UserName, userFromView.Password);
                }
                else
                {
                    appUser = await UserManager.FindAsync(userFromView.UserName, userFromView.Password);
                }

                if (appUser == null)
                {
                    throw new Exception("Invalid login or password.");
                }

                var claim = await UserManager.CreateIdentityAsync(
                    appUser,
                    DefaultAuthenticationTypes.ApplicationCookie
                );

                if (claim == null)
                {
                    throw new Exception("Can't create an identity claim.");
                }

                result.ResultData = claim;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
            }

            return result;
        }

        public bool IsUserNameAvailable(string userName)
        {
            bool result = _context.Users
                .Where(u => u.UserName.Equals(userName))
                .SingleOrDefault() == null;

            return result;
        }

        public bool IsEmailAvailable(string email)
        {
            bool result = _context.Users
                .Where(u => u.Email.Equals(email))
                .SingleOrDefault() == null;

            return result;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                    _userManager?.Dispose();
                    _roleManager?.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}