using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LazyVocabulary.Web.Startup))]
namespace LazyVocabulary.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}