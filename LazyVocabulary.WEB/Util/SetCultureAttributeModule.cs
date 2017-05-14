using LazyVocabulary.Logic.Services;
using LazyVocabulary.Web.Filters;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc.FilterBindingSyntax;
using System.Web.Mvc;

namespace LazyVocabulary.Web.Util
{
    public class SetCultureAttributeModule : NinjectModule
    {
        [Inject]
        public UserProfileService service { get; set; }

        public SetCultureAttributeModule()
        {
        }

        public override void Load()
        {
            this.BindFilter<SetCultureAttribute>(FilterScope.Controller, 0)
                .WhenControllerHas<SetCultureAttribute>()
                .WithConstructorArgument("service", service);
        }
    }
}