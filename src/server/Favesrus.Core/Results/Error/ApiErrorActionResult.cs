using Favesrus.Core.Results.Base;
using System.Net;

namespace Favesrus.API.Results.Error
{
    public class ApiErrorActionResult : BaseActionResult
    {
        public ApiErrorActionResult(string apiStatus, string message, HttpStatusCode badRequest = HttpStatusCode.BadRequest)
            :base(apiStatus, message, badRequest)
        {

        }
    }

    public class ApiErrorActionResult<T>:BaseActionResult<T>
    {
        public ApiErrorActionResult(string apiStatus, string message, T entity, HttpStatusCode badRequest = HttpStatusCode.BadRequest)
            : base(apiStatus, message, entity, badRequest)
        {

        }
    }
}
