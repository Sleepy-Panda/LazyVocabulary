using LazyVocabulary.Logic.Services;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LazyVocabulary.RestAPI.Controllers
{
    public class AuthController : ApiController
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

        [HttpGet]
        public async Task<IHttpActionResult> GetAuthTokenAsync(string email, string password)
        {
            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password)) {
                return BadRequest();
            }

            var resultWithData = await UserService.GetAuthTokenAsync(email, password);

            if (!resultWithData.Success)
            {
                return BadRequest();
            }

            var token = resultWithData.ResultData;

            return Ok(new { success = true, token = token });
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetUserNameByTokenAsync(string token)
        {
            if (String.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            var resultWithData = await UserService.GetUserNameByTokenAsync(token);

            if (!resultWithData.Success)
            {
                return BadRequest();
            }

            var userName = resultWithData.ResultData;

            return Ok(new { success = true, userName = userName });
        }
    }
}
