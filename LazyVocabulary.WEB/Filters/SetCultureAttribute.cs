using LazyVocabulary.Common.Entities;
using LazyVocabulary.Common.Enums;
using LazyVocabulary.Logic.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Ninject;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace LazyVocabulary.Web.Filters
{
    public class SetCultureAttribute : FilterAttribute, IActionFilter
    {
        private UserService _userService;

        public UserService UserService
        {
            get
            {
                return _userService ?? HttpContext.Current.GetOwinContext().Get<UserService>();
            }
            private set
            {
                _userService = value;
            }
        }

        public SetCultureAttribute()
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            CultureInfo cultureInfo;

            // Get culture from cookies.
            HttpCookie cultureFromCookie = filterContext.HttpContext.Request.Cookies["locale"];

            if (cultureFromCookie == null)
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    cultureInfo = UserProfile.DefaultCulture;
                }
                else
                {
                    // Get culture from database and set cookie.
                    var resultWithData = UserService.GetCultureByUserId(filterContext.HttpContext.User.Identity.GetUserId());

                    if (!resultWithData.Success)
                    {
                        cultureInfo = UserProfile.DefaultCulture;
                    }

                    cultureInfo = resultWithData.ResultData;
                }

                AddOrUpdateLocaleCookie(filterContext, cultureInfo.Name.ToLower());
            }
            else
            {
                // Invalid cookie.
                if (!Enum.GetNames(typeof(LocaleLanguage)).Any(l => l.ToLower() == cultureFromCookie.Value.ToLower()))
                {
                    cultureInfo = UserProfile.DefaultCulture;
                    AddOrUpdateLocaleCookie(filterContext, cultureInfo.Name.ToLower());
                }
                else
                {
                    cultureInfo = CultureInfo.CreateSpecificCulture(cultureFromCookie.Value);
                }
            }

            // Formatting data: dates, numbers etc.
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            // Resources localization.
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Without implementation.
        }

        private void AddOrUpdateLocaleCookie(ActionExecutedContext filterContext, string locale)
        {
            HttpCookie cookie = filterContext.HttpContext.Request.Cookies["locale"];

            if (cookie == null)
            {
                cookie = new HttpCookie("locale");
            }

            cookie.Value = locale;
            cookie.Expires = DateTime.Now.AddDays(30);

            filterContext.HttpContext.Response.Cookies.Add(cookie);
        }
    }
}