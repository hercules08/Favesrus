using Favesrus.Core.Results.Base;
using System.Net;

namespace Favesrus.Results
{
    public class ApiActionResult: BaseActionResult
    {
        public ApiActionResult(string apiStatus, string message, HttpStatusCode okStatus = HttpStatusCode.OK)
            :base(apiStatus, message, okStatus)
        {

        }
    }

    public class ApiActionResult<T>:BaseActionResult<T>
    {
        public ApiActionResult(string apiStatus, string message, T entity, HttpStatusCode okStatus = HttpStatusCode.OK)
            : base(apiStatus, message, entity, okStatus)
        {

        }
    }
}
