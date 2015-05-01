using Faves = Favesrus.Common;
using Favesrus.Model.Entity;
using Favesrus.Server.Dto.FavesrusUser;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Processing;
using Favesrus.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Host.SystemWeb;
using System.Web;
using Favesrus.Server.Exceptions;
using Favesrus.Common;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Favesrus.Server.Filters;
using Favesrus.Server.Processing.ActionResult;
using System;
using Favesrus.Server.Controllers.WebApi;
using System.Net.Http;
using System.Web.Http;
using Favesrus.Server.Processing.Interface;
using Favesrus.Services.Interfaces;

namespace Favesrus.Server.Processing.Impl
{
    public class FavesrusUserProcessor : BaseProcessor, IFavesrusUserProcessor
    {
        public FavesrusUserProcessor(
            IEmailer emailer,
            IAutoMapper mapper)
            : base(emailer, mapper)
        {

        }

        public FavesrusUserProcessor(
            FavesrusUserManager userManager,
            FavesrusRoleManager roleManager,
            IAuthenticationManager authManager,
            IEmailer emailer,
            IAutoMapper mapper)
            :base(userManager, roleManager, authManager, emailer, mapper)
        {

        }

        public DtoFavesrusUser RegisterUser(RegisterModel model)
        {
            FavesrusUser user = Mapper.Map<FavesrusUser>(model);
            user.UserName = model.Email; // In Faves user name is email addy.            

            IdentityResult step_1_result;
            IdentityResult step_2_result;
            DtoFavesrusUser dtoFavesrusUser;


            // Step 1 - Create the user
            step_1_result = UserManager.Create(user, model.Password);
            if (!step_1_result.Succeeded)
            {
                var errors = GetErrorsFromIdentityResult(step_1_result);
                throw new BusinessRuleException(
                    Faves.Constants.Status.UNABLE_TO_CREATE_USER,
                    "Unable to register new Faves 'R' Us user.",
                    errors);
            }

            // Step 2 - Add the user to the customer role
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
            return System.Threading.Tasks.Task.FromResult(RegisterUser(model));
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
            return System.Threading.Tasks.Task.FromResult(LoginUser(model));
        }

        public Task<IHttpActionResult> LoginFacebookAsync(LoginFacebookModel model, HttpRequestMessage requestMessage, AccountController controller)
        {
            return System.Threading.Tasks.Task.FromResult(LoginFacebook(model, requestMessage, controller));
        }

        public IHttpActionResult LoginFacebook(LoginFacebookModel model, HttpRequestMessage requestMessage, AccountController controller)
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