using Favesrus.Domain.Entity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Favesrus.Services
{
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<FavesrusUser, string>
    {
        public ApplicationSignInManager(FavesrusUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(FavesrusUser user)
        {
            return user.GenerateUserIdentityAsync((FavesrusUserManager)UserManager);
        }
    }
}
