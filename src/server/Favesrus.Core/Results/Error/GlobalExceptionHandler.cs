using Favesrus.Core.Results.Error;
using System.Net;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.IdentityModel.Tokens;

namespace Favesrus.API.Results.Error
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;

            var httpException = exception as HttpException;

            if (httpException != null)
            {
                context.Result = 
                    new ApiErrorActionResult("HttpException", 
                        httpException.Message, 
                        (HttpStatusCode)httpException.GetHttpCode());
                return;
            }

            if (exception is SecurityTokenValidationException)
            {
                context.Result =
                     new ApiErrorActionResult("Unauthorized",
                        exception.Message,
                        HttpStatusCode.Unauthorized);
                return;
            }
            
            try
            {
                ApiErrorException apiErrorException = exception as ApiErrorException;

                if (exception is ApiErrorException)
                {
                    if (apiErrorException.Entity == null)
                        context.Result = new ApiErrorActionResult(apiErrorException.ApiStatus, apiErrorException.Message, apiErrorException.StatusCode);
                    else
                        context.Result = new ApiErrorActionResult<object>(apiErrorException.ApiStatus, apiErrorException.Message, apiErrorException.Entity, apiErrorException.StatusCode);
                    return;
                }
            }
            catch
            {
                return;
            }

            context.Result = new ApiErrorActionResult("ServerError",exception.Message,HttpStatusCode.InternalServerError);
        }
    }
}