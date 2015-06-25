using Favesrus.Data.Dtos;
using Favesrus.Results;
using Favesrus.Server.Dto.FavesrusUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.API.Controllers
{
    public partial interface IAccountController
    {
        Task<IHttpActionResult> RegisterAsync(RegisterModel model);
    }

    public partial class AccountController : IAccountController
    {
        public async Task<IHttpActionResult> RegisterAsync(RegisterModel model)
        {
            string apiStatus = "successful_register";
            string apiMessage = "Successfully registered Faves 'R' Us user.";

            Logger.Info(string.Format("Attempt register as: {0}|with password: {1}", model.Email, model.Password));

            FavesrusUserModel dtoFavesrusUser = await _accountService.RegisterUserAsync(model);

            return new ApiActionResult<FavesrusUserModel>(apiStatus,apiMessage, dtoFavesrusUser);
        }
    }
}
