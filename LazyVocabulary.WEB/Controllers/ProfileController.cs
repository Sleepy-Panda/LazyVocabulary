using LazyVocabulary.Logic.Helpers;
using LazyVocabulary.Logic.Services;
using LazyVocabulary.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LazyVocabulary.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private UserService _userService;

        public ProfileController()
        {
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

            var resultWithDataProfile = await UserService.GetProfileByUserId(userId);
            var resultWithDataUser = await UserService.GetByUserId(userId);
            var resultWithDataEmail = await UserService.GetEmailByUserId(userId);

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
                SubscribersCount = user.SubscriberSubscriptions.Count,
                SubscriptionsCount = user.TargetSubscriptions.Count,
            };

            return View(model);
        }

        // GET: Profile/Overview?ownerId=1
        [HttpGet]
        public async Task<ActionResult> Overview(string ownerId)
        {
            string userId = User.Identity.GetUserId();

            var resultWithDataProfile = await UserService.GetProfileByUserId(ownerId);
            var resultWithDataUser = await UserService.GetByUserId(ownerId);

            if (!resultWithDataProfile.Success || !resultWithDataUser.Success)
            {
                // TODO
            }

            var profile = resultWithDataProfile.ResultData;
            var user = resultWithDataUser.ResultData;

            var model = new OverviewProfileViewModel
            {
                OwnerId = ownerId,
                Name = profile.Name,
                Surname = profile.Surname,
                DateOfBirth = profile.DateOfBirth?.ToString("d MMMM, yyyy"),
                CreatedAt = profile.CreatedAt.ToString("d MMMM, yyyy HH:mm"),
                AvatarImagePath = GetAvatarImagePath(ownerId),
                UserName = User.Identity.Name,
                // TODO only public
                DictionariesCount = user.Dictionaries.Count,
                SubscribersCount = user.SubscriberSubscriptions.Count,
                SubscriptionsCount = user.TargetSubscriptions.Count,
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