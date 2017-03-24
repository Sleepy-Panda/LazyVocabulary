using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace LazyVocabulary.WEB.Filters
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

            if (exception is HttpException)
            {
                // как посмотреть статусный код?
                exceptionContext.HttpContext.Response.Clear();
                exceptionContext.HttpContext.Response.StatusCode = 404;
            }

            exceptionContext.ExceptionHandled = true;
        }
    }
}