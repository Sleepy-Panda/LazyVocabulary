using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using LazyVocabulary.BLL.Identity;
using LazyVocabulary.BLL.Services;
using System.Threading.Tasks;
using LazyVocabulary.BLL.DTO;
using LazyVocabulary.WEB.Models;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System;

namespace LazyVocabulary.WEB.Controllers
{
    public class AccountController : Controller
    {
        private UserService _userService;
        //private ApplicationSignInManager _signInManager;
        private IAuthenticationManager _authenticationManager;

        public AccountController()
        {
        }

        public AccountController(UserService userService, ApplicationSignInManager signInManager)
        {
            UserService = userService;
            //SignInManager = signInManager;
        }

        public UserService UserService
        {
            get
            {
                return _userService ?? HttpContext.GetOwinContext().Get<UserService>();
            }
            private set
            {
                _userService = value;
            }
        }

        /*public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }*/

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _authenticationManager ?? HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new UserDTO
            {
                Email = model.Login,
                UserName = model.Login,
                Password = model.Password,
            };

            var resultWithData = await UserService.CreateIdentityAsync(user);
            var claim = resultWithData.ResultData;

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                },
                claim
            );

            return RedirectToLocal(returnUrl);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Register
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new UserDTO {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
            };

            var result = await UserService.CreateUserAsync(user);

            if (result.Success)
            {
                var resultWithData = await UserService.CreateIdentityAsync(user);
                var claim = resultWithData.ResultData;

                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                    }, 
                    claim
                );

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", result.Message);
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult IsUserNameAvailable(string userName)
        {
            bool result = UserService.IsUserNameAvailable(userName);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult IsEmailAvailable(string email)
        {
            bool result = UserService.IsEmailAvailable(email);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userService != null)
                {
                    _userService.Dispose();
                    _userService = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}