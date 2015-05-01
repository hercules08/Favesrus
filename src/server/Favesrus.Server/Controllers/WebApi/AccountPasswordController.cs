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
using Favesrus.Server.Processing;

namespace Favesrus.Server.Controllers.WebApi
{
    public partial class AccountController : ApiBaseController
    {
        [HttpPost]
        [AllowAnonymous]
        [ValidateModel]
        public IHttpActionResult ForgotPassword(HttpRequestMessage requestMessage, ForgotPasswordViewModel model)
        {
            var user = UserManager.FindByEmail(model.Email);
            if (user == null)
            {
                var entity = new InvalidModelProperty("issue1", "The user does not exist.");
                throw new BusinessRuleException("user_not_registered.", "User not registered with Faves", entity);
            }

            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link
            string code = UserManager.GeneratePasswordResetToken(user.Id);
            var callbackUrl = Url.Link("ResetPassword", new { controller = "Account", userId = user.Id, code = code });
            new EmailService()
                .SendEmail(Favesrus.Common.Constants.EMAIL_ADDRESS, "Reset Faves 'R' Us Password", "Please confirm your Faves password reset by clicking <a href=\"" + callbackUrl + "\">here</a>", user.Email);
            UserManager.SendEmail(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
            return new BaseActionResult<string>(requestMessage, "Email sent", "Email sent", "forgot_password_email_sent");
        }

        [HttpGet]
        [Route("resetpassword", Name = "ResetPassword")]
        public void ResetPassword(string userId, string code)
        {
            if (userId == null || code == null)
            {
                Log.Info("User Id or Generated Code was empty. When resetting");
                //return BadRequest("User Id or Generated Code was empty.");
            }

            //TODO Check that the user actually exists
            var user = UserManager.FindById(userId);
            if (user != null)
            {
                var result = UserManager.ResetPassword(userId, code, "tempsorary1");
                if (result.Succeeded)
                {
                    EmailService emailSender = new EmailService();
                    emailSender.SendEmail(
                        Favesrus.Common.Constants.EMAIL_ADDRESS,
                        "Faves Password Reset",
                        "Your Faves account password has been reset", user.Email);
                    Log.Info(string.Format("Successfully confirmed {0}", user.UserName));
                }
            }
        }
    }
}
