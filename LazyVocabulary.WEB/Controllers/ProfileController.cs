using LazyVocabulary.Logic.Helpers;
using LazyVocabulary.Logic.Services;
using LazyVocabulary.Web.Filters;
using LazyVocabulary.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LazyVocabulary.Web.Controllers
{
    [Authorize]
    [SetCulture]
    public class ProfileController : Controller
    {
        private UserService _userService;
        private SubscriptionService _subscriptionService;
        private UserProfileService _userProfileService;

        public ProfileController(SubscriptionService service, UserProfileService userProfileService)
        {
            _subscriptionService = service;
            _userProfileService = userProfileService;
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

        // GET: Profile
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string userId = User.Identity.GetUserId();

            var resultWithDataProfile = await UserService.GetProfileByUserIdAsync(userId);
            var resultWithDataUser = await UserService.GetByUserIdAsync(userId);
            var resultWithDataEmail = await UserService.GetEmailByUserIdAsync(userId);
            var resultWithDataSubscribersCount = _subscriptionService.GetSubscribersCountByUserId(userId);
            var resultWithDataSubscribtionsCount = _subscriptionService.GetSubscriptionsCountByUserId(userId);

            if (!resultWithDataProfile.Success)
            {
                // TODO
            }

            var profile = resultWithDataProfile.ResultData;
            var user = resultWithDataUser.ResultData;

            var model = new IndexProfileViewModel
            {
                Id = profile.Id,
                Name = profile.Name,
                Surname = profile.Surname,
                DateOfBirth = profile.DateOfBirth?.ToString("d MMMM, yyyy"),
                CreatedAt = profile.CreatedAt.ToString("d MMMM, yyyy HH:mm"),
                UpdatedAt = profile.UpdatedAt.ToString("d MMMM, yyyy HH:mm"),
                AvatarImagePath = GetAvatarImagePath(userId),
                UserName = User.Identity.Name,
                Email = resultWithDataEmail.ResultData,
                DictionariesCount = user.Dictionaries.Count,
                SubscribersCount = resultWithDataSubscribersCount.ResultData,
                SubscriptionsCount = resultWithDataSubscribtionsCount.ResultData,
            };

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            var userId = User.Identity.GetUserId();
            var resultWithDataProfile = await UserService.GetProfileByUserIdAsync(userId);

            if (!resultWithDataProfile.Success)
            {
                // TODO
            }

            var profile = resultWithDataProfile.ResultData;

            var model = new EditProfileViewModel
            {
                Id = profile.Id,
                Name = profile.Name,
                Surname = profile.Surname,
                DateOfBirth = profile.DateOfBirth?.ToString("dd.MM.yyyy"),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Result = "Error";
                return PartialView("_Edit", model);
            }

            dynamic profile = new ExpandoObject();
            profile.Id = model.Id;
            profile.Name = model.Name;
            profile.Surname = model.Surname;
            profile.DateOfBirth = model.DateOfBirth != null 
                ? DateTime.Parse(model.DateOfBirth)
                : (DateTime?) null;

            var result = await _userProfileService.UpdateAsync(profile);

            if (!result.Success)
            {
                ViewBag.Result = "Error";
                return PartialView("_Edit", model);
            }

            ViewBag.Result = "Success";
            return PartialView("_Edit");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            var model = new ChangePasswordViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Result = "Error";
                return PartialView("_ChangePassword", model);
            }

            var userId = User.Identity.GetUserId();

            dynamic user = new ExpandoObject();
            user.UserId = userId;
            user.Password = model.Password;
            user.NewPassword = model.NewPassword;

            var result = await UserService.ChangePasswordAsync(user);

            if (!result.Success)
            {
                ViewBag.Result = "Error";
                return PartialView("_ChangePassword", model);
            }

            await UserService.SetPasswordUpdatedAtForProfileAsync(userId);

            ViewBag.Result = "Success";
            return PartialView("_ChangePassword");
        }

        // GET: Profile/Overview?ownerId=1
        [HttpGet]
        public async Task<ActionResult> Overview(string targetUserId)
        {
            if (String.IsNullOrEmpty(targetUserId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.GetUserId();

            if (userId == targetUserId)
            {
                return RedirectToAction("Index");
            }

            var resultWithDataProfile = await UserService.GetProfileByUserIdAsync(targetUserId);
            var resultWithDataUser = await UserService.GetByUserIdAsync(targetUserId);
            var resultWithDataUserName = await UserService.GetUserNameByUserId(userId);
            var resultWithDataSubscribersCount = _subscriptionService.GetSubscribersCountByUserId(targetUserId);
            var resultWithDataSubscribtionsCount = _subscriptionService.GetSubscriptionsCountByUserId(targetUserId);
            var resultWithDataCanSubscribe = _subscriptionService.CanSubscribe(userId, targetUserId);

            if (!resultWithDataProfile.Success || !resultWithDataUser.Success || !resultWithDataUserName.Success)
            {
                // TODO
            }

            var profile = resultWithDataProfile.ResultData;
            var user = resultWithDataUser.ResultData;

            var model = new OverviewProfileViewModel
            {
                TargetUserId = targetUserId,
                Name = profile.Name,
                Surname = profile.Surname,
                DateOfBirth = profile.DateOfBirth?.ToString("d MMMM, yyyy"),
                CreatedAt = profile.CreatedAt.ToString("d MMMM, yyyy HH:mm"),
                AvatarImagePath = GetAvatarImagePath(targetUserId),
                UserName = resultWithDataUserName.ResultData,
                // TODO only public
                DictionariesCount = user.Dictionaries.Count,
                SubscribersCount = resultWithDataSubscribersCount.ResultData,
                SubscriptionsCount = resultWithDataSubscribtionsCount.ResultData,
                CanSubscribe = resultWithDataCanSubscribe.ResultData,
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult ChangeAvatar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ChangeAvatar(HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    string userId = User.Identity.GetUserId();
                    var folder = Server.MapPath(ConfigurationHelper.AvatarFolder);

                    var existingFile = Directory
                    .GetFiles(folder, $"{ userId }.*", SearchOption.TopDirectoryOnly)
                    .SingleOrDefault();

                    if (existingFile != null)
                    {
                        System.IO.File.Delete(existingFile);
                    }

                    string fileName = $"{ userId }{ Path.GetExtension(file.FileName) }";
                    string path = Path.Combine(folder, fileName);
                    file.SaveAs(path);

                    await UserService.SetUpdatedAtForProfileAsync(userId);
                }
            }
            catch (Exception ex)
            {
                // TODO
            }

            return RedirectToAction("Index", "Profile");
        }

        private string GetAvatarImagePath(string userId)
        {
            try
            {
                var folder = Server.MapPath(ConfigurationHelper.AvatarFolder);
                var path = Directory
                    .GetFiles(folder, $"{ userId }.*", SearchOption.TopDirectoryOnly)
                    .SingleOrDefault();

                if (String.IsNullOrEmpty(path))
                {
                    return null;
                }

                return Path.Combine(ConfigurationHelper.AvatarFolder, Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}