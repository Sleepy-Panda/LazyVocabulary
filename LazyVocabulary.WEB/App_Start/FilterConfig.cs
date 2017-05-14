using LazyVocabulary.Web.Filters;
using System.Web.Mvc;

namespace LazyVocabulary.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RedirectOnExceptionAttribute());
            //filters.Add(new SetCultureAttribute());
        }
    }
}
