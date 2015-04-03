using Favesrus.Model.Entity;
using Favesrus.Services;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Favesrus.Server.Infrastructure
{
    public class FavesrusAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task GrantResourceOwnerCredentials(
            OAuthGrantResourceOwnerCredentialsContext context)
        {
            FavesrusUserManager favesUserMgr =
                context.OwinContext.Get<FavesrusUserManager>("AspNet.Identity.Owin:" +
                typeof(FavesrusUserManager).AssemblyQualifiedName);

            FavesrusUser user = await favesUserMgr.FindAsync(context.UserName,
                context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant",
                    "The username or password is incorrect");
            }
            else
            {
                ClaimsIdentity ident = await favesUserMgr.CreateIdentityAsync(user, "Custom");
                AuthenticationTicket ticket =
                    new AuthenticationTicket(ident, new AuthenticationProperties());
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(ident);
            }
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }
    }
}