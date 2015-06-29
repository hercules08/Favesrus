using Favesrus.Core.Results.Error;
using Favesrus.Data.Dtos;
using Favesrus.Domain.Entity;
using Favesrus.Server.Dto.FavesrusUser;
using Favesrus.Services;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using Favesrus.Core.TypeMapping;
using Favesrus.Core;
using Favesrus.Core.Logging;
using Microsoft.Owin.Security.Cookies;
using System.Collections.Generic;
using System;
using Favesrus.Results;
using System.Web.Http;

namespace Favesrus.ApiService
{
    public interface IAccountService
    {
        Task<FavesrusUserModel> RegisterUserAsync(RegisterModel model);
        Task<FavesrusUserModel> LoginUserAsync(LoginModel model);
        Task<IHttpActionResult> LoginFacebookAsync(LoginFacebookModel model, BaseApiController controller);
        bool LogOut();
    }

    public class AccountService:BaseService, IAccountService
    {
        ApplicationSignInManager _signInManager;
        FavesrusUserManager _userManager;
        IAuthenticationManager _authManager;
        ICommunicationService _commService;
        IEmailService _emailService;

        public AccountService(
            IAutoMapper mapper,
            ILogManager logManager,
            ApplicationSignInManager signInManager,
            FavesrusUserManager userManager,
            IAuthenticationManager authManager,
            ICommunicationService commService,
            IEmailService emailService)
            :base(logManager,mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authManager = authManager;
            _commService = commService;
            _emailService = emailService;
        }
        
        public async Task<FavesrusUserModel> LoginUserAsync(LoginModel model)
        {
            Logger.Info(string.Format("Find user with email: {0}", model.Email));
            FavesrusUser user = await _userManager.FindByNameAsync(model.Email);

            if(user != null)
            {
                Logger.Info(string.Format("Found user: {0}| Id: {1}", user.FirstName, user.Id));
                // 1. Check password
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: true, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        Logger.Info("Successful login.");
                        return Mapper.Map<FavesrusUserModel>(user);
                    case SignInStatus.Failure:
                    default:
                        string errorMessage = string.Format("Unable to sign in {0}. Please check password.",user.FirstName);
                        Logger.Error(errorMessage);
                        throw new ApiErrorException(errorMessage);
                    //TODO: Include these options
                    //case SignInStatus.LockedOut:
                    //    return View("Lockout");
                    //case SignInStatus.RequiresVerification:
                    //    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                }
            }
            else
            {
                string errorMessage = "Login User Model is null.";
                Logger.Error(errorMessage);
                model.Password = "*******";
                throw new ApiErrorException(errorMessage,"unable_to_locate_user",model);
            }
        }

        public async Task<IHttpActionResult> LoginFacebookAsync(LoginFacebookModel model, BaseApiController controller)
        {
            FavesrusUser user = _userManager.FindByName(model.Email);

            if (user == null) // User is not registered
            {
                Logger.Info(string.Format("User {0} is not registered.", model.Email));
                user = new FavesrusUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                // Create default user wishlist
                if (user.WishLists == null)
                    user.WishLists = new List<WishList>();
                user.WishLists.Add(new WishList() { WishListName = "Default" });

                IdentityResult result = _userManager.Create(user);

                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = _userManager.GenerateEmailConfirmationToken(user.Id);
                    try
                    {
                        var callbackUrl = controller.Url.Link("ConfirmFacebookEmail", new { userId = user.Id, code = code, providerKey = model.ProviderKey });
                        //UserManager.SendEmail(user.Id, "Confirm Faves 'R' Us Account", "Please confirm your Faves account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        _emailService.SendEmail(FavesrusConstants.EMAIL_ADDRESS, "Confirm Faves 'R' Us Account", "Please confirm your Faves account by clicking <a href=\"" + callbackUrl + "\">here</a>", user.Email);
                        return new ApiActionResult("facebook_register_email_sent",string.Format("Email sent to {0}", user.Email));
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorException("Unable to facebook login.", ex);
                    }
                }
                else
                {
                    throw new ApiErrorException("unable_to_create_facebook_user", "Unable to create user from facebook", result);
                }
            }
            else // User is registered
            {
                Logger.Info(string.Format("User {0} is registered.", model.Email));
                UserLoginInfo loginInfo = new UserLoginInfo(FavesrusConstants.FACEBOOK_PROVIDER, model.ProviderKey);

                FavesrusUser userWithValidLoginInfo = _userManager.Find(loginInfo);

                if (userWithValidLoginInfo != null) // Users login info is in DB 
                {
                    ClaimsIdentity ident = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    _authManager.SignOut();
                    _authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = true }, ident);

                    var dtoFavesUser = Mapper.Map<FavesrusUserModel>(user);

                    //return dtoFavesUser;
                    return new ApiActionResult<FavesrusUserModel>("facebook_login_success", "Facebook login success.", dtoFavesUser);
                }
                else // Users login info is not in DB
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = _userManager.GenerateEmailConfirmationToken(user.Id);
                    try
                    {
                        var callbackUrl = controller.Url.Link("ConfirmFacebookEmail", new { userId = user.Id, code = code, providerKey = model.ProviderKey });
                        //_userManager.SendEmail(user.Id, "Confirm Faves 'R' Us Account", "Please confirm your Faves account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        _emailService.SendEmail(FavesrusConstants.EMAIL_ADDRESS, "Confirm Faves 'R' Us Account", "Please confirm your Faves account by clicking <a href=\"" + callbackUrl + "\">here</a>", user.Email);
                        return new ApiActionResult("facebook_register_email_sent", string.Format("Email sent to {0}", user.Email));
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message);
                    }
                }
            }

            Logger.Info(string.Format("Confirm email request sent for: {0}|with provider key: {1}", model.Email, model.ProviderKey));
            return new ApiActionResult("facebook_register_email_sent", string.Format("Email sent to {0}", user.Email));
        }

        public bool LogOut()
        {
            bool success = false;

            try
            {
                _authManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
                success = true;
            }
            catch
            { }

            return success;
        }



        public async Task<FavesrusUserModel> RegisterUserAsync(RegisterModel model)
        {
            FavesrusUser user = Mapper.Map<FavesrusUser>(model);
            user.UserName = model.Email; // In Faves user name is email addy.            

            IdentityResult step_1_result;
            IdentityResult step_2_result;
            FavesrusUserModel dtoFavesrusUser;


            // Step 1 - Create default wishlist for user
            if (user.WishLists == null)
                user.WishLists = new List<WishList>();
            user.WishLists.Add(new WishList() { WishListName = "Default" });

            // Step 2 - Create the user
            step_1_result = _userManager.Create(user, model.Password);
            if (!step_1_result.Succeeded)
            {
                var errors = GetErrorsFromIdentityResult(step_1_result);
                throw new ApiErrorException(
                    FavesrusConstants.Status.UNABLE_TO_CREATE_USER,
                    "Unable to register new Faves 'R' Us user.",
                    errors);
            }

            // Step 3 - Add the user to the customer role
            step_2_result = _userManager.AddToRole(user.Id, FavesrusConstants.CUSTOMER_ROLE);
            if (!step_2_result.Succeeded)
            {
                var errors = GetErrorsFromIdentityResult(step_1_result);
                throw new ApiErrorException(
                    FavesrusConstants.Status.UNABLE_TO_ADD_USER_TO_ROLE,
                    "Unable to add the newly created user to the customer role.",
                    errors);
            }

            // End Result
            dtoFavesrusUser = Mapper.Map<FavesrusUserModel>(user);

            return dtoFavesrusUser;
        }
    }
}
