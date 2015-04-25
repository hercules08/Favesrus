using Favesrus.Model.Entity;
using Favesrus.Server.Dto.FavesrusUser;
using Favesrus.Server.ErrorHandling;
using Favesrus.Server.Exceptions;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Models;
using Favesrus.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Faves = Favesrus.Common;
using System.Net.Mail;
using System.Web;
using Favesrus.Server.Filters;
using Favesrus.Server.Processing.ProcessingFavesrusUser.Interface;
using Favesrus.Server.Processing.ProcessingFavesrusUser.ActionResult;

namespace Favesrus.Server.Controllers.WebApi
{
    public partial class AccountController : ApiBaseController
    {
        [HttpPost]
        [Route("register")]
        [ValidateModel]
        public async Task<IHttpActionResult> Register(HttpRequestMessage requestMessage, RegisterModel model)
        {
            string successMessage = "Successfully registered Faves 'R' Us user.";

            Log.Info(string.Format("Attempt register as {0} with password {1}", model.Email, model.Password));

            DtoFavesrusUser dtoFavesrusUser = await _favesrusUserProcessor.RegisterUserAsync(model);

            var result = new RegisterDtoFavesrusActionResult(
                requestMessage,
                dtoFavesrusUser,
                successMessage);

            return result;
        } 
    }
}
