using Favesrus.Model.Entity;
using FavesApi = Favesrus.Server.Controllers.WebApi;
using Favesrus.Server.Dto.FavesrusUser;
using Favesrus.Server.Exceptions;
using Favesrus.Server.Filters;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Models;
using Favesrus.Server.Processing.Interface;
using Favesrus.Services;
using Favesrus.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Faves = Favesrus.Common;
using Angela.Core;
using System.IO;
using System.Collections.Generic;

namespace Favesrus.Server.Processing.Impl
{
    public class AcccountProcessor : BaseProcessor, IAccountProcessor
    {
        public AcccountProcessor(
            IEmailer emailer,
            IAutoMapper mapper)
            : base(emailer, mapper)
        {

        }

        public AcccountProcessor(
            FavesrusUserManager userManager,
            FavesrusRoleManager roleManager,
            IAuthenticationManager authManager,
            IEmailer emailer,
            IAutoMapper mapper)
            : base(userManager, roleManager, authManager, emailer, mapper)
        {

        }

        public DtoFavesrusUser RegisterUser(RegisterModel model)
        {
            FavesrusUser user = Mapper.Map<FavesrusUser>(model);
            user.UserName = model.Email; // In Faves user name is email addy.            

            IdentityResult step_1_result;
            IdentityResult step_2_result;
            DtoFavesrusUser dtoFavesrusUser;


            // Step 1 - Create default wishlist for user
            if (user.WishLists == null)
                user.WishLists = new List<WishList>();
            user.WishLists.Add(new WishList() { WishListName = "Default" });

            // Step 2 - Create the user
            step_1_result = UserManager.Create(user, model.Password);
            if (!step_1_result.Succeeded)
            {
                var errors = GetErrorsFromIdentityResult(step_1_result);
                throw new BusinessRuleException(
                    Faves.Constants.Status.UNABLE_TO_CREATE_USER,
                    "Unable to register new Faves 'R' Us user.",
                    errors);
            }

            // Step 3 - Add the user to the customer role
            step_2_result = UserManager.AddToRole(user.Id, Faves.Constants.CUSTOMER_ROLE);
            if (!step_2_result.Succeeded)
            {
                var errors = GetErrorsFromIdentityResult(step_1_result);
                throw new BusinessRuleException(
                    Faves.Constants.Status.UNABLE_TO_ADD_USER_TO_ROLE,
                    "Unable to add the newly created user to the customer role.",
                    errors);
            }

            // End Result
            dtoFavesrusUser = Mapper.Map<DtoFavesrusUser>(user);

            return dtoFavesrusUser;
        }

        public Task<DtoFavesrusUser> RegisterUserAsync(RegisterModel model)
        {
            return Task.FromResult(RegisterUser(model));
        }

        public DtoFavesrusUser LoginUser(LoginModel model)
        {
            FavesrusUser user = UserManager.FindByName(model.Email);

            if (user != null)
            {
                var result = UserManager.CheckPassword(user, model.Password);

                if (result != false)
                {
                    ClaimsIdentity ident = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);

                    var dtoFavesUser = Mapper.Map<DtoFavesrusUser>(user);

                    return dtoFavesUser;

                }

                throw new BusinessRuleException("incorrect_user_password", "Incorrect user password!");
            }
            else
            {
                throw new BusinessRuleException("no_user_found", "User not found in Faves 'R' Us");
            }
        }

        public Task<DtoFavesrusUser> LoginUserAsync(LoginModel model)
        {
            return Task.FromResult(LoginUser(model));
        }

        public IHttpActionResult ResetPassword(string userId, string code, HttpRequestMessage requestMessage)
        {
            if (userId == null || code == null)
            {
                Log.Info("User Id or Generated Code was empty. When resetting");
                throw new BusinessRuleException(
                    "userid_or_code_missing_status",
                    "User id or code was null", new { userId = userId, code = code });
            }

            //TODO Check that the user actually exists
            var user = UserManager.FindById(userId);
            if (user != null)
            {
                var temporaryPassword = Path.GetRandomFileName().Replace(".", "").Substring(0, 6);

                var result = UserManager.ResetPassword(userId, code, temporaryPassword);
                if (result.Succeeded)
                {
                    EmailService emailSender = new EmailService();
                    emailSender.SendEmail(
                        Favesrus.Common.Constants.EMAIL_ADDRESS,
                        "Faves Password Reset",
                        "Your Faves account password has been reset to: " + temporaryPassword, user.Email);
                    Log.Info(string.Format("Successfully reset password {0}", user.UserName));
                    return new BaseActionResult<string>(
                        requestMessage,
                        "",
                        "Temporary password created",
                        "password_reset_success_status");
                }
                else
                {
                    throw new BusinessRuleException(
                    "unable_to_reset_password",
                    "Unable to reset user password.");
                }
            }
            else
            {
                throw new BusinessRuleException(
                    "user_does_not_exist_status",
                    "The user does not exist in the system.");
            }
        }

        public Task<IHttpActionResult> ResetPasswordAsync(string userId, string code, HttpRequestMessage requestMessage)
        {
            return Task.FromResult(ResetPassword(userId, code, requestMessage));
        }

        public IHttpActionResult ForgotPassword(ForgotPasswordViewModel model,
            HttpRequestMessage requestMessage,
            FavesApi.AccountController controller)
        {
            var user = UserManager.FindByEmail(model.Email);
            if (user == null)
            {
                var entity = new InvalidModelProperty("issue1", "The user does not exist.");
                throw new BusinessRuleException("user_not_registered.", "User not registered with Faves", entity);
            }

            // For more information on how to enable account confirmation and password reset please visit
            // http://go.microsoft.com/fwlink/?LinkID=320771

            // Send an email with this link
            string code = UserManager.GeneratePasswordResetToken(user.Id);
            var callbackUrl = controller.Url.Link("ResetPassword",
                new
                {
                    controller = "Account",
                    userId = user.Id,
                    code = code
                });
            new EmailService()
                .SendEmail(Faves.Constants.EMAIL_ADDRESS,
                "Reset Faves 'R' Us Password",
                "Please confirm your Faves password reset by clicking <a href=\"" + callbackUrl + "\">here</a>",
                user.Email);
            UserManager.SendEmail(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
            return new BaseActionResult<string>(requestMessage, "Email sent", "Email sent", "forgot_password_email_sent");
        }

        public Task<IHttpActionResult> ForgotPasswordAsync(ForgotPasswordViewModel model,
            HttpRequestMessage requestMessage,
            FavesApi.AccountController controller)
        {
            return Task.FromResult(ForgotPassword(model, requestMessage, controller));
        }

        public IHttpActionResult LoginFacebook(LoginFacebookModel model,
            HttpRequestMessage requestMessage,
            FavesApi.AccountController controller)
        {
            FavesrusUser user = UserManager.FindByName(model.Email);

            if (user == null) // User is not registered
            {
                Log.Info(string.Format("User {0} is not registered.", model.Email));
                user = new FavesrusUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                // Create default user wishlist
                if (user.WishLists == null)
                    user.WishLists = new List<WishList>();
                user.WishLists.Add(new WishList() { WishListName = "Default" });

                IdentityResult result = UserManager.Create(user);

                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = UserManager.GenerateEmailConfirmationToken(user.Id);
                    try
                    {
                        var callbackUrl = controller.Url.Link("ConfirmFacebookEmail", new { userId = user.Id, code = code, providerKey = model.ProviderKey });
                        //UserManager.SendEmail(user.Id, "Confirm Faves 'R' Us Account", "Please confirm your Faves account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        Emailer.SendEmail(Favesrus.Common.Constants.EMAIL_ADDRESS, "Confirm Faves 'R' Us Account", "Please confirm your Faves account by clicking <a href=\"" + callbackUrl + "\">here</a>", user.Email);
                        return new BaseActionResult<string>(requestMessage, "Email sent", string.Format("Email sent to {0}", user.Email), "facebook_register_email_sent");
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }
                }
                else
                {
                    throw new BusinessRuleException("unable_to_create_facebook_user", "Unable to create user from facebook", result);
                }
            }
            else // User is registered
            {
                Log.Info(string.Format("User {0} is registered.", model.Email));
                UserLoginInfo loginInfo = new UserLoginInfo(Faves.Constants.FACEBOOK_PROVIDER, model.ProviderKey);

                FavesrusUser userWithValidLoginInfo = UserManager.Find(loginInfo);

                if (userWithValidLoginInfo != null) // Users login info is in DB 
                {
                    ClaimsIdentity ident = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(
                        new AuthenticationProperties { IsPersistent = true }, ident);

                    var dtoFavesUser = Mapper.Map<DtoFavesrusUser>(user);

                    //return dtoFavesUser;
                    return new BaseActionResult<DtoFavesrusUser>(requestMessage, dtoFavesUser, "Facebook login success.", "facebook_login_success");
                }
                else // Users login info is not in DB
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = UserManager.GenerateEmailConfirmationToken(user.Id);
                    try
                    {
                        var callbackUrl = controller.Url.Link("ConfirmFacebookEmail", new { userId = user.Id, code = code, providerKey = model.ProviderKey });
                        //UserManager.SendEmail(user.Id, "Confirm Faves 'R' Us Account", "Please confirm your Faves account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        EmailService emailSender = new EmailService();
                        emailSender.SendEmail(Favesrus.Common.Constants.EMAIL_ADDRESS, "Confirm Faves 'R' Us Account", "Please confirm your Faves account by clicking <a href=\"" + callbackUrl + "\">here</a>", user.Email);
                        return new BaseActionResult<string>(requestMessage, "Email sent", string.Format("Email sent to {0}", user.Email), "facebook_register_email_sent");
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }
                }
            }

            Log.Info(string.Format("Confirm email request sent for {0} with provider key {1}", model.Email, model.ProviderKey));
            return new BaseActionResult<string>(requestMessage, "Email sent", string.Format("Email sent to {0}", user.Email), "facebook_register_email_sent");
        }


        public Task<IHttpActionResult> LoginFacebookAsync(
            LoginFacebookModel model, 
            HttpRequestMessage requestMessage, 
            FavesApi.AccountController controller)
        {
            return Task.FromResult(LoginFacebook(model, requestMessage, controller));
        }
    }
}















































































































//private void AddErrors(IdentityResult result)
//{
//    foreach (var error in result.Errors)
//    {
//        int separatorIndex = model.Key.IndexOf(".") + 1; //breaks up the 'model.ProviderKey' to just 'ProviderKey'
//        string modelKey = model.Key;
//        InvalidModelProperty invalidItem = new InvalidModelProperty();
//        invalidItem.ErrorItem = modelKey.Remove(0, separatorIndex);
//        invalidItem.Reason = model.Value.Errors[0].ErrorMessage;
//        invalidModelStates.Add(invalidItem);

//        ModelState.AddModelError("", error);
//    }
//}

//private void AddErrorsFromResult(IdentityResult result)
//{
//    foreach (string error in result.Errors)
//    {
//        ModelState.AddModelError("", error);
//    }
//}

//private IHttpActionResult GetErrorResult(IdentityResult result)
//{
//    if (result == null)
//    {
//        return InternalServerError();
//    }

//    if (!result.Succeeded)
//    {
//        if (result.Errors != null)
//        {
//            foreach (string error in result.Errors)
//            {
//                ModelState.AddModelError("", error);
//            }
//        }

//        if (ModelState.IsValid)
//        {
//            // No ModelState errors are available to send, so just return an empty BadRequest.
//            return BadRequest();
//        }

//        return BadRequest(ModelState);
//    }

//    return null;
//}