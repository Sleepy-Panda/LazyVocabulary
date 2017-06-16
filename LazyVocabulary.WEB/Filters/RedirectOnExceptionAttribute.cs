using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace LazyVocabulary.Web.Filters
{
    public class RedirectOnExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.ExceptionHandled)
            {
                return;
            }

            Exception exception = exceptionContext.Exception;

            if (exception == null)
            {
                return;
            }

            if (exception is TargetInvocationException)
            {
                exception = exception.InnerException;
            }

            // Internal Server Error.
            var code = 500;

            if (exception is HttpException)
            {
                code = (exception as HttpException).GetHttpCode();
                exceptionContext.HttpContext.Response.Clear();
                exceptionContext.HttpContext.Response.StatusCode = code;
            }

            exceptionContext.ExceptionHandled = true;
            exceptionContext.HttpContext.Response.Redirect("/Home/Index");
        }
    }
}