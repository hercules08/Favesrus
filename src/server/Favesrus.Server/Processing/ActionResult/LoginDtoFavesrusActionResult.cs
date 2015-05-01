using Favesrus.Server.Dto.FavesrusUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Favesrus.Server.Processing.ActionResult
{
    public class LoginDtoFavesrusActionResult : BaseActionResult<DtoFavesrusUser>
    {
        public LoginDtoFavesrusActionResult(
            HttpRequestMessage requestMessage,
            DtoFavesrusUser entity,
            string message,
            string status, HttpStatusCode responseStatus)
            :base(requestMessage, entity, message, status, responseStatus)
        {

        }
    }
}