using Favesrus.Core;
using Favesrus.Core.Results.Error;
using Favesrus.Data.RequestModels;
using Favesrus.Results;
using Favesrus.Services;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.API.Controllers
{
    public partial interface IAccountController
    {
        Task<IHttpActionResult> ResetPasswordAsync(string userId, string code);
        Task<IHttpActionResult> ForgotPasswordAsync(ForgotPasswordViewModel model, AccountController controller);
    }

    public partial class AccountController:IAccountController
    {
        public async Task<IHttpActionResult> ResetPasswordAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                Logger.Info("User Id or Generated Code was empty. When resetting");
                throw new ApiErrorException(
                    "userid_or_code_missing_status",
                    "User id or code was null", new { userId = userId, code = code });
            }

            //TODO Check that the user actually exists
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var temporaryPassword = Path.GetRandomFileName().Replace(".", "").Substring(0, 6);

                var result = await _userManager.ResetPasswordAsync(userId, code, temporaryPassword);
                if (result.Succeeded)
                {
                    EmailService emailSender = new EmailService();
                    emailSender.SendEmail(
                        FavesrusConstants.EMAIL_ADDRESS,
                        "Faves Password Reset",
                        "Your Faves account password has been reset to: " + temporaryPassword, user.Email);
                    Logger.Info(string.Format("Successfully reset password {0}", user.UserName));
                    return new ApiActionResult("password_reset_success_status","Temporary password created");
                }
                else
                {
                    throw new ApiErrorException(
                    "unable_to_reset_password",
                    "Unable to reset user password.");
                }
            }
            else
            {
                throw new ApiErrorException(
                    "user_does_not_exist_status",
                    "The user does not exist in the system.");
            };
        }

        public async Task<IHttpActionResult> ForgotPasswordAsync(ForgotPasswordViewModel model, AccountController controller)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                var entity = new InvalidModelProperty("issue1", "The user does not exist.");
                throw new ApiErrorException("user_not_registered.", "User not registered with Faves", entity);
            }

            // For more information on how to enable account confirmation and password reset please visit
            // http://go.microsoft.com/fwlink/?LinkID=320771

            // Send an email with this link
            string code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
            var callbackUrl = controller.Url.Link("ResetPassword",
                new
                {
                    controller = "Account",
                    userId = user.Id,
                    code = code
                });
            new EmailService()
                .SendEmail(FavesrusConstants.EMAIL_ADDRESS,
                "Reset Faves 'R' Us Password",
                "Please confirm your Faves password reset by clicking <a href=\"" + callbackUrl + "\">here</a>",
                user.Email);
            //UserManager.SendEmail(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
            return new ApiActionResult("forgot_password_email_sent","Email sent");
        }
    }
}
