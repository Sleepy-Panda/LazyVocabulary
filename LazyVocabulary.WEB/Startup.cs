using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LazyVocabulary.WEB.Startup))]
namespace LazyVocabulary.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}