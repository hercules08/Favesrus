using Favesrus.Core;
using Favesrus.Server.Exceptions;
using Favesrus.Server.Filters;
using Favesrus.Server.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.IdentityModel.Tokens;

namespace Favesrus.Server.ErrorHandling
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;

            var httpException = exception as HttpException;

            if (httpException != null)
            {
                context.Result = new SimpleErrorResult(context.Request,
                    (HttpStatusCode)httpException.GetHttpCode(), httpException.Message);
                return;
            }

            if (exception is SecurityTokenValidationException)
            {
                context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.Unauthorized,
                    exception.Message);
                return;
            }

            var baseErrorException = exception as BaseErrorException;

            if (exception is RootObjectNotFoundException)
            {
                context.Result =
                    new ErrorActionResult(
                        context.Request,
                        FavesrusConstants.Status.ROOT_OBJECT_NOT_FOUND,
                        baseErrorException.Message,
                        baseErrorException.Entity);
                return;
            }

            if (exception is ChildObjectNotFoundException)
            {
                context.Result =
                    new ErrorActionResult(
                        context.Request,
                        FavesrusConstants.Status.CHILD_OBJECT_NOT_FOUND,
                        baseErrorException.Message,
                        baseErrorException.Entity);
                return;
            }

            if (exception is BusinessRuleException)
            {
                var businessRuleException = exception as BusinessRuleException;

                context.Result =
                    new ErrorActionResult(
                        context.Request,
                        businessRuleException.Status,
                        businessRuleException.Message,
                        businessRuleException.Entity);
                return;
            }

            context.Result = new SimpleErrorResult(context.Request, HttpStatusCode.InternalServerError,
                exception.Message);
        }
    }
}