using LazyVocabulary.Logic.Services;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(LazyVocabulary.RestAPI.Startup))]
namespace LazyVocabulary.RestAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<UserService>(CreateUserService);

            app.UseCors(CorsOptions.AllowAll);
        }

        private UserService CreateUserService()
        {
            return new UserService("DefaultConnection");
        }
    }
}
