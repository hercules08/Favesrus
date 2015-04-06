using Favesrus.Common;
using Favesrus.Server.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Favesrus.Server.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid != true)
            {
                List<InvalidModelProperty> invalidModelStates = new List<InvalidModelProperty>();

                var modelState = actionContext.ModelState;
                foreach(var model in modelState)
                {

                    int separatorIndex = model.Key.IndexOf(".")+1; //breaks up the 'model.ProviderKey' to just 'ProviderKey'
                    string modelKey = model.Key;
                    InvalidModelProperty invalidItem = new InvalidModelProperty();
                    invalidItem.ErrorItem = modelKey.Remove(0,separatorIndex);
                    invalidItem.Reason = model.Value.Errors[0].ErrorMessage;
                    invalidModelStates.Add(invalidItem);
                }

                actionContext.Response =
                    new ErrorActionResult(
                        actionContext.Request,
                        Constants.Status.INVALID_MODELSTATE,
                        "Bad model state",
                        invalidModelStates).Execute();

                    //new InvalidModelStateActionResult(
                    //    actionContext.Request,invalidModelStates).Execute();
            }
        }

        /// <summary>
        /// To prevent filter from executing twice on same call. Problem solved by:
        /// http://stackoverflow.com/questions/18485479/webapi-filter-is-calling-twice?rq=1
        /// </summary>
        public override bool AllowMultiple
        {
            get { return false; }
        }
    }
}