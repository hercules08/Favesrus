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

namespace Favesrus.ApiService
{
    public interface IAccountService
    {
        Task<FavesrusUserModel> LoginUserAsync(LoginModel model);
    }

    public class AccountService:BaseService, IAccountService
    {
        ApplicationSignInManager _signInManager;
        FavesrusUserManager _userManager;

        public AccountService(
            IAutoMapper mapper,
            ILogManager logManager,
            ApplicationSignInManager signInManager,
            FavesrusUserManager userManager)
            :base(logManager,mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        public async Task<FavesrusUserModel> LoginUserAsync(LoginModel model)
        {
            Logger.Info(string.Format("Find user with email: {0}", model.Email));
            FavesrusUser user = await _userManager.FindByEmailAsync(model.Email);

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
                throw new ApiErrorException(errorMessage, model);
            }
        }
    }
}
