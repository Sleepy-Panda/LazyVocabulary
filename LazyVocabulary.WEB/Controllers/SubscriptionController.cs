using LazyVocabulary.Logic.Helpers;
using LazyVocabulary.Logic.Services;
using LazyVocabulary.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LazyVocabulary.Web.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private UserService _userService;
        private SubscriptionService _subscriptionService;

        public SubscriptionController(SubscriptionService service)
        {
            _subscriptionService = service;
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

        // POST: Subscribe
        [HttpPost]
        public async Task<ActionResult> Subscribe(string targetUserId)
        {
            if (String.IsNullOrEmpty(targetUserId))
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            string userId = User.Identity.GetUserId();

            var result = await _subscriptionService.SubscribeAsync(userId, targetUserId);

            if (!result.Success)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        // POST: Subscribe
        [HttpPost]
        public async Task<ActionResult> Unsubscribe(string targetUserId)
        {
            if (String.IsNullOrEmpty(targetUserId))
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            string userId = User.Identity.GetUserId();

            var result = await _subscriptionService.UnsubscribeAsync(userId, targetUserId);

            if (!result.Success)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> Subscriptions()
        {
            string userId = User.Identity.GetUserId();

            var resultWithData = _subscriptionService.GetSubscriptionsByUserId(userId);

            if (!resultWithData.Success)
            {
                // TODO
            }

            var subscriptions = resultWithData.ResultData;

            var model = new List<SubscriptionViewModel>();

            foreach (var subscription in subscriptions)
            {
                var resultWithDataUserName = await UserService.GetUserNameByUserId(userId);

                if (!resultWithDataUserName.Success)
                {
                    continue;
                }

                var userName = resultWithDataUserName.ResultData;
                var resultWithDataId = await UserService.GetIdByUserName(userName);

                if (resultWithDataId.Success)
                {
                    continue;
                }

                var targetUserId = resultWithDataId.ResultData;

                var modelItem = new SubscriptionViewModel()
                {
                    UserId = targetUserId,
                    Name = subscription.Name,
                    Surname = subscription.Surname,
                    UserName = userName,
                    AvatarImagePath = GetAvatarImagePath(targetUserId),
                };

                model.Add(modelItem);
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Subscribers()
        {
            string userId = User.Identity.GetUserId();

            var resultWithData = _subscriptionService.GetSubscribersByUserId(userId);

            if (!resultWithData.Success)
            {
                // TODO
            }

            var subscribers = resultWithData.ResultData;

            var model = new List<SubscriptionViewModel>();

            foreach (var subscriber in subscribers)
            {
                var resultWithDataUserName = await UserService.GetUserNameByUserId(userId);

                if (!resultWithDataUserName.Success)
                {
                    continue;
                }

                var userName = resultWithDataUserName.ResultData;
                var resultWithDataId = await UserService.GetIdByUserName(userName);

                if (resultWithDataId.Success)
                {
                    continue;
                }

                var subscriberUserId = resultWithDataId.ResultData;

                var modelItem = new SubscriptionViewModel()
                {
                    UserId = subscriberUserId,
                    Name = subscriber.Name,
                    Surname = subscriber.Surname,
                    UserName = userName,
                    AvatarImagePath = GetAvatarImagePath(subscriberUserId),
                };

                model.Add(modelItem);
            }

            return View();
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