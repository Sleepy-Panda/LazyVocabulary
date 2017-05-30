using LazyVocabulary.Logic.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LazyVocabulary.Web.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private SubscriptionService _subscriptionService;

        public SubscriptionController(SubscriptionService service)
        {
            _subscriptionService = service;
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
    }
}