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

namespace Favesrus.Server.Controllers.WebApi
{
    [RoutePrefix("api/account")]
    public class AccountController : BaseController
    {
        private IAutoMapper mapper;

        public AccountController(IAutoMapper mapper)
            : base()
        {
            this.mapper = mapper;
        }

        public AccountController
            (IAutoMapper mapper,
                FavesrusUserManager userManager,
                FavesrusRoleManager roleManager,
                IAuthenticationManager authManager)
            : base(userManager, roleManager, authManager)
        {
            this.mapper = mapper;
        }

        //[HttpPost]
        //[Route("registerfacebook")]
        //public async Task<IHttpActionResult> RegisterFacebook(RegisterFacebookModel model)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    UserLoginInfo loginInfo =
        //        new UserLoginInfo(Faves.Constants.FACEBOOK_PROVIDER,
        //                model.ProviderKey);

        //    FavesrusUser user = await UserManager.FindAsync(loginInfo);

        //    if (user == null)
        //    {
        //        user = mapper.Map<FavesrusUser>(model);
        //        user.UserName = model.Email;

        //        return await RegisterUser(user, loginInfo, null);
        //    }
        //    else
        //    {
        //        return BadRequest("The user already exists!");
        //    }
        //}

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FavesrusUser user = mapper.Map<FavesrusUser>(model);
            user.UserName = model.Email;

            return await RegisterUser(user, null, model.Password);
        }

        private async Task<IHttpActionResult> RegisterUser(FavesrusUser user, UserLoginInfo loginInfo = null, string password = null)
        {
            IdentityResult result;
            IHttpActionResult errorResult;

            if (string.IsNullOrEmpty(password))
            {
                result = await UserManager.CreateAsync(user);

                // Unable to create user
                if (!result.Succeeded)
                    return GetErrorResult(result);

                result = await UserManager.AddLoginAsync(user.Id, loginInfo);
            }
            else
            {
                result = await UserManager.CreateAsync(user, password);
            }

            errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                //return errorResult;


                //Dictionary<string, object> error = new Dictionary<string, object>();
                //error.Add("Message", "Username is already taken");
                ////error.Add("ErrorMessage", "Something really bad happened");
                ////return BadRequest(error);

                //HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Username is already taken");

                return BadRequest("Username is already taken.");
            }

            // Add to customer role
            if (!RoleManager.RoleExists(Faves.Constants.CUSTOMER_ROLE))
                await RoleManager.CreateAsync(new FavesrusRole(Faves.Constants.CUSTOMER_ROLE));

            await UserManager.AddToRoleAsync(user.Id, Faves.Constants.CUSTOMER_ROLE);

            var dtoFavesrusUser = mapper.Map<DtoFavesrusUser>(user);

            return Ok(dtoFavesrusUser);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login(HttpRequestMessage request, LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FavesrusUser user = await UserManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var result = await UserManager.CheckPasswordAsync(user, model.Password);

                if (result != false)
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);

                    var dtoFavesUser = mapper.Map<DtoFavesrusUser>(user);

                    return Ok(dtoFavesUser);
                }

                return BadRequest("Incorrect user password!");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("loginfacebook")]
        public async Task<IHttpActionResult> LoginFacebook(HttpRequestMessage request, LoginFacebookModel model)
        {
            Log.Info(string.Format("Attempt register as {0} with provider key {1}", model.Email, model.ProviderKey));

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            FavesrusUser user = await UserManager.FindByNameAsync(model.Email);

            if (user == null) // User is not registered
            {
                Log.Info(string.Format("User {0} is not registered.", model.Email));
                user = new FavesrusUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                IdentityResult result = await UserManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = UserManager.GenerateEmailConfirmationToken(user.Id);
                    try
                    {
                        var callbackUrl = Url.Link("ConfirmFacebookEmail", new { userId = user.Id, code = code, providerKey = model.ProviderKey });
                        //UserManager.SendEmail(user.Id, "Confirm Faves 'R' Us Account", "Please confirm your Faves account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        Emailer emailSender = new Emailer();
                        emailSender.SendEmail(Favesrus.Common.Constants.EMAIL_ADDRESS, "Confirm Faves 'R' Us Account", "Please confirm your Faves account by clicking <a href=\"" + callbackUrl + "\">here</a>", user.Email);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }
                }
                else
                {
                    return GetErrorResult(result);
                }
            }
            else // User is registered
            {
                Log.Info(string.Format("User {0} is registered.", model.Email));
                UserLoginInfo loginInfo = new UserLoginInfo(Faves.Constants.FACEBOOK_PROVIDER, model.ProviderKey);

                FavesrusUser userWithValidLoginInfo = await UserManager.FindAsync(loginInfo);

                if (userWithValidLoginInfo != null) // Users login info is in DB 
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(
                        new AuthenticationProperties { IsPersistent = true }, ident);

                    var dtoFavesUser = mapper.Map<DtoFavesrusUser>(user);

                    return Ok(dtoFavesUser);
                }
                else // Users login info is not in DB
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = UserManager.GenerateEmailConfirmationToken(user.Id);
                    try
                    {
                        var callbackUrl = Url.Link("ConfirmFacebookEmail", new { userId = user.Id, code = code, providerKey = model.ProviderKey });
                        //UserManager.SendEmail(user.Id, "Confirm Faves 'R' Us Account", "Please confirm your Faves account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        Emailer emailSender = new Emailer();
                        emailSender.SendEmail(Favesrus.Common.Constants.EMAIL_ADDRESS, "Confirm Faves 'R' Us Account", "Please confirm your Faves account by clicking <a href=\"" + callbackUrl + "\">here</a>", user.Email);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                    }
                }
            }

            Log.Info(string.Format("Confirm email request sent for {0} with provider key {1}", model.Email, model.ProviderKey));
            return Ok("Confirm email request sent.");
        }

        [HttpGet]
        [Route("confirmfacebookemail",Name="ConfirmFacebookEmail")]
        //[Route("confirmfacebookemail/userid={userId}&code={code}&providerkey={providerKey}", Name="ConfirmFacebookEmail")]
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
                    Emailer emailSender = new Emailer();
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

        [HttpPost]
        [Authorize]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            AuthManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public void ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.FindByEmail(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "The user does not exist.");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = UserManager.GeneratePasswordResetToken(user.Id);
                var callbackUrl = Url.Route("DefaultApi", new { controller = "Account", userId = user.Id, code = code });
                UserManager.SendEmail(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");

            }
        }
    }
}
