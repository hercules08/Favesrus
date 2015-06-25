using Favesrus.API.Filters;
using Favesrus.Core;
using Favesrus.Core.Results.Error;
using Favesrus.Data.Dtos;
using Favesrus.Results;
using Favesrus.Server.Dto.FavesrusUser;
using Favesrus.Services;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.API.Controllers
{
    public partial interface IAccountController
    {
        Task<IHttpActionResult> LoginAsync(LoginModel model);
        Task<IHttpActionResult> LoginFacebook(LoginFacebookModel model, BaseApiController controller);
    }

    public partial class AccountController: IAccountController
    {
        /// <summary>
        /// Log In to Faves 'R' Us with your email and password
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("login")]
        [ValidateModel]
        public async Task<IHttpActionResult> LoginAsync(LoginModel model)
        {
            Logger.Info("Begin Login");

            string apiStatus = "login_success";
            string apiMessage = "Successfully logged in to Faves 'R' Us.";

            FavesrusUserModel userModel = await _accountService.LoginUserAsync(model);

            Logger.Info("End Login");

            return new ApiActionResult<FavesrusUserModel>(apiStatus, apiMessage, userModel);
        }

        /// <summary>
        /// Log In to Faves 'R' us with Facebook
        /// </summary>
        [HttpPost]
        [Route("loginfacebook")]
        [ValidateModel]
        public async Task<IHttpActionResult> LoginFacebook(LoginFacebookModel model, BaseApiController controller)
        {
            Logger.Info(string.Format("Attempt register as: {0}|Provider key: {1}", model.Email, model.ProviderKey));

            var result = await _accountService.LoginFacebookAsync(model, controller);

            return result as IHttpActionResult;
        }

        /// <summary>
        /// Confirms Facebook Faves 'R' Us sign in
        /// </summary>
        /// <param name="userId">The Faves 'R' Us users id</param>
        /// <param name="code">The auto-generated confirm code sent with the email</param>
        /// <param name="providerKey">The newly generated provider key from the users device</param>
        [HttpGet]
        [Route("confirmfacebookemail", Name = "ConfirmFacebookEmail")]
        public void ConfirmFacebookEmail(string userId, string code, string providerKey)
        {
            if (userId == null || code == null)
            {
                Logger.Info("User Id or Generated Code was empty.");
                throw new ApiErrorException("User Id or Generated Code was empty.");
            }

            var result = _userManager.ConfirmEmail(userId, code);
            if (result.Succeeded)
            {

                var user = _userManager.FindById(userId);
                //var dtoFavesUser = mapper.Map<DtoFavesrusUser>(user);
                //await UserManager.SendEmailAsync(user.Id, "Faves Account Confirmation", "Your Faves Account Has Been Confirmed");

                UserLoginInfo loginInfo =
                    new UserLoginInfo(FavesrusConstants.FACEBOOK_PROVIDER,
                            providerKey);


                var addLoginResult = _userManager.AddLogin(user.Id, loginInfo);

                if (addLoginResult.Succeeded)
                {
                    EmailService emailSender = new EmailService();
                    emailSender.SendEmail(
                        FavesrusConstants.EMAIL_ADDRESS,
                        "Faves Account Confirmed",
                        "Your Faves account has been confirmed", user.Email);
                    Logger.Info(string.Format("Successfully confirmed {0}", user.UserName));
                }
                else
                {
                    Logger.Info(string.Format("Unable to add login for: {0}| with provider key: {1}", user.UserName, providerKey));
                }
                //return Ok(dtoFavesUser);
            }
            else
            {
                Logger.Info(string.Format("Error finding user with Id: {0}", userId));
                //return GetErrorResult(result);
            }
        }
    }
}
