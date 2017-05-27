using LazyVocabulary.Logic.Services;
using LazyVocabulary.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
        public async Task<ActionResult> Index()
        {
            string userId = User.Identity.GetUserId();

            var resultWithData = await UserService.GetProfileByUserId(userId);

            if (!resultWithData.Success)
            {
                // TODO
            }

            var profile = resultWithData.ResultData;

            var model = new IndexProfileViewModel
            {
                Id = profile.Id,
                Name = profile.Name,
                Surname = profile.Surname,
                DateOfBirth = profile.DateOfBirth?.ToString("d MMMM, yyyy"),
                CreatedAt = profile.CreatedAt.ToString("d MMMM, yyyy HH:mm"),
                UpdatedAt = profile.UpdatedAt.ToString("d MMMM, yyyy HH:mm"),
                AvatarImagePath = profile.AvatarImagePath,
                //UserName = profile.UserName,
                //Email = profile.Email,
            };

            return View(model);
        }
    }
}