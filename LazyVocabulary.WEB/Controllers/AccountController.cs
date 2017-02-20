using LazyVocabulary.BLL.Interfaces;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using LazyVocabulary.BLL.Identity;
using LazyVocabulary.BLL.Services;
using System.Threading.Tasks;
using LazyVocabulary.BLL.DTO;

namespace LazyVocabulary.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private ApplicationSignInManager _signInManager;
        private IAuthenticationManager _authenticationManager;

        private IUserService UserService
        {
            get
            {
                return _userService ?? HttpContext.GetOwinContext().Get<IUserService>();
            }
        }

        private ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _authenticationManager ?? HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Account/Login
        [AllowAnonymous]
        public async Task<ActionResult> Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            var user = new UserDTO { UserName = "llol@gmail.com", Email = "llol@gmail.com" };
            UserService.Create(user);

            return View();
        }
    }
}