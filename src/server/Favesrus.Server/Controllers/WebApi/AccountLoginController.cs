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
using Favesrus.Server.Processing.Interface;
using Favesrus.Server.Processing.ActionResult;

namespace Favesrus.Server.Controllers.WebApi
{
    public partial class AccountController : ApiBaseController
    {
        /// <summary>
        /// Log In to Faves 'R' Us with your email and password
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("login")]
        [ValidateModel]
        public async Task<IHttpActionResult> Login(HttpRequestMessage request, LoginModel model)
        {
            string successStatus = "login_success";
            string successMessage = "Successfully logged in to Faves 'R' Us.";

            DtoFavesrusUser dtoFavesrusUser = await _accountProcessor.LoginUserAsync(model);

            var result = new LoginDtoFavesrusActionResult(
                request,dtoFavesrusUser,
                successMessage,successStatus, 
                HttpStatusCode.OK);

            return result;
        }

        /// <summary>
        /// Log In to Faves 'R' us with Facebook
        /// </summary>
        [HttpPost]
        [Route("loginfacebook")]
        [ValidateModel]
        public async Task<IHttpActionResult> LoginFcebook(HttpRequestMessage requestMessage, LoginFacebookModel model)
        {
            Log.Info(string.Format("Attempt register as {0} with provider key {1}", model.Email, model.ProviderKey));

            var result = await _accountProcessor.LoginFacebookAsync(model, requestMessage, this);

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
                Log.Info("User Id or Generated Code was empty.");
                //return BadRequest("User Id or Generated Code was empty.");
            }

            var result = UserManager.ConfirmEmail(userId, code);
            if (result.Succeeded)
            {

                var user = UserManager.FindById(userId);
                //var dtoFavesUser = mapper.Map<DtoFavesrusUser>(user);
                //await UserManager.SendEmailAsync(user.Id, "Faves Account Confirmation", "Your Faves Account Has Been Confirmed");

                UserLoginInfo loginInfo =
                    new UserLoginInfo(Faves.Constants.FACEBOOK_PROVIDER,
                            providerKey);


                var addLoginResult = UserManager.AddLogin(user.Id, loginInfo);

                if (addLoginResult.Succeeded)
                {
                    EmailService emailSender = new EmailService();
                    emailSender.SendEmail(
                        Favesrus.Common.Constants.EMAIL_ADDRESS,
                        "Faves Account Confirmed",
                        "Your Faves account has been confirmed", user.Email);
                    Log.Info(string.Format("Successfully confirmed {0}", user.UserName));
                }
                else
                {
                    Log.Info(string.Format("Unable to add login for {0} with provider key {1}", user.UserName, providerKey));
                }
                //return Ok(dtoFavesUser);
            }
            else
            {
                Log.Info(string.Format("Error finding user with Id: {0}", userId));
                //return GetErrorResult(result);
            }
        }
    }
}
