using LazyVocabulary.BLL.Helpers;
using LazyVocabulary.BLL.Services;
using LazyVocabulary.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LazyVocabulary.WEB.Controllers
{
    public class AccountController : Controller
    {
        private UserService _userService;
        private UserProfileService _userProfileService;

        public AccountController(UserProfileService service)
        {
            _userProfileService = service;
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

        public UserProfileService UserProfileService
        {
            get
            {
                return _userProfileService;
            }
            private set
            {
                _userProfileService = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

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

            dynamic user = new ExpandoObject();
            user.UserName = user.Email = model.Login;
            user.Password = model.Password;

            var resultWithData = await UserService.CreateIdentityAsync(user);

            if (!resultWithData.Success)
            {
                ModelState.AddModelError("", resultWithData.Message);
                return View(model);
            }

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

            return RedirectToAction("Login", "Account");
        }

        // GET: Account/Register
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

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

            dynamic user = new ExpandoObject();
            user.UserName = model.UserName;
            user.Password = model.Password;
            user.Email = model.Email;
            user.Locale = HttpContext.Request.Cookies["locale"]?.Value;

            // Create application user.
            var resultWithStringData = await UserService.CreateUserAsync(user);

            if (!resultWithStringData.Success)
            {
                ModelState.AddModelError("", resultWithStringData.Message);
                return View(model);
            }

            // Log in created user using claim.
            var resultWithClaimData = await UserService.CreateIdentityAsync(user);

            if (!resultWithClaimData.Success)
            {
                ModelState.AddModelError("", resultWithClaimData.Message);
                return View(model);
            }

            var claim = resultWithClaimData.ResultData;

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                }, 
                claim
            );

            // Send message to user's email.
            var text = String.Format("Для подтверждения email перейдите по ссылке: " +
                "<a href=\"{0}\" title=\"Подтвердить адрес электронной почты\">Подтвердить адрес электронной почты</a>",
                Url.Action("VerifyToken", "Account", new { token = "asd", email = model.Email }, Request.Url.Scheme));

            var result = await EmailHelper.SendEmail(model.Email, "Подтверждение email", text);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
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