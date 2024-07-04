using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using StoryTrails.Comunication.Exceptions;

namespace StoryTrails.API.Filters
{
    public class ExceptionFilter:IExceptionFilter
    {

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AppException)
            {
                ThrowNewException(context);
            }
            else
            {
                ThrowUnknownException(context);
            }
        }

        private void ThrowNewException(ExceptionContext context)
        {
            var cashflowException = (AppException)context.Exception;
            context.HttpContext.Response.StatusCode = cashflowException.statusCode;
            context.Result = new ObjectResult(cashflowException.getError());


        }
        private void ThrowUnknownException(ExceptionContext context)
        {
            var errorRespone = "Unknown Error";
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorRespone);
        }
}
}
