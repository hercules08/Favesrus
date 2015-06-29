using Favesrus.ApiService;
using Favesrus.Core;
using Favesrus.Core.Logging;
using Favesrus.Core.Results.Error;
using Favesrus.Core.TypeMapping;
using Favesrus.Data.Dtos;
using Favesrus.Results;
using Favesrus.Server.Dto.FavesrusUser;
using Favesrus.Services;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.API.Controllers
{
    public partial interface IAccountController
    {
        IHttpActionResult Logout();
    }

    [RoutePrefix("api/account")]
    public partial class AccountController:BaseApiController, IAccountController
    {
        IAccountService _accountService;
        FavesrusUserManager _userManager;
        IEmailService _emailService;

        public AccountController(ILogManager logManager, IAutoMapper mapper, IAccountService accountService,
            FavesrusUserManager userManager, IEmailService emailService)
            : base(logManager, mapper)
        {
            _accountService = accountService;
            _userManager = userManager;
            _emailService = emailService;
        }

        // POST api/Account/Logout
        [Authorize]
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Logger.Info("Begin Logout");

            string apiStatus = "logout_success";
            string apiMessage = "Successfully logged out of Faves 'R' Us.";

            bool logOutSuccess = _accountService.LogOut();

            if (logOutSuccess)
            {
                Logger.Info("Logout success");
                return new ApiActionResult(apiStatus, apiMessage);
            }
            else
            {
                string errorMessage = "Unable to log out";
                Logger.Error(errorMessage);
                throw new ApiErrorException(errorMessage);
            }
        }
    }
}
