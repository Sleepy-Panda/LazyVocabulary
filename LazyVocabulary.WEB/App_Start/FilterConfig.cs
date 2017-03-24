using LazyVocabulary.WEB.Filters;
using System.Web;
using System.Web.Mvc;

namespace LazyVocabulary.WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new RedirectOnExceptionAttribute());
        }
    }
}
