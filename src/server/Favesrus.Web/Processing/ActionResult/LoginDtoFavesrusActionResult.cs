using Favesrus.Data.Dtos;
using System.Net;
using System.Net.Http;

namespace Favesrus.Server.Processing.ActionResult
{
    public class LoginDtoFavesrusActionResult : BaseActionResult<FavesrusUserModel>
    {
        public LoginDtoFavesrusActionResult(
            HttpRequestMessage requestMessage,
            FavesrusUserModel entity,
            string message,
            string status, HttpStatusCode responseStatus)
            :base(requestMessage, entity, message, status, responseStatus)
        {

        }
    }
}