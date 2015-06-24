using Favesrus.ApiService;
using Favesrus.Core.Logging;
using Favesrus.Data.Dtos;
using Favesrus.Results;
using Favesrus.Server.Dto.FavesrusUser;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.API.Controllers
{
    public interface IAccountController
    {
        Task<IHttpActionResult> LoginAsync(LoginModel model);
    }

    public class AccountController:BaseApiController, IAccountController
    {

        IAccountService _accountService;

        public AccountController(ILogManager logManager, IAccountService accountService)
            :base(logManager)
        {
            _accountService = accountService;
        }

        public async Task<IHttpActionResult> LoginAsync(LoginModel model)
        {
            Logger.Info("Begin Login");

            string apiStatus = "login_success";
            string apiMessage = "Successfully logged in to Faves 'R' Us.";

            FavesrusUserModel userModel = await _accountService.LoginUserAsync(model);

            Logger.Info("End Login");

            return new ApiActionResult<FavesrusUserModel>(apiStatus, apiMessage, userModel);
        }
    }
}
