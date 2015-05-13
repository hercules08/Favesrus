using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Processing.Interface;
using Favesrus.Services;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Web.Http;

namespace Favesrus.Server.Controllers.WebApi
{
    [RoutePrefix("api/account")]
    public partial class AccountController : ApiBaseController
    {
        private IAutoMapper _mapper;
        private IAccountProcessor _accountProcessor;

        //public AccountController()
        //    :base()
        //{

        //}

        public AccountController(IAutoMapper mapper,
            IAccountProcessor favesrusUserProcessor)
        {
            _mapper = mapper;
            _accountProcessor = favesrusUserProcessor;
        }

        public AccountController
            (IAutoMapper mapper,
                IAccountProcessor favesrusUserProcessor,
                FavesrusUserManager userManager,
                FavesrusRoleManager roleManager,
                IAuthenticationManager authManager)
            : base(userManager, roleManager, authManager)
        {
            _mapper = mapper;
            _accountProcessor = favesrusUserProcessor;
        }

        /// <summary>
        /// Sign out of Faves 'R' Us
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            AuthManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok("Successful sign out.");
        }

    }
}
