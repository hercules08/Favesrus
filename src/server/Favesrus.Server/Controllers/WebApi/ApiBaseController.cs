using Favesrus.Model.Entity;
using Favesrus.Services;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;


namespace Favesrus.Server.Controllers.WebApi
{
    public abstract class ApiBaseController : ApiController
    {
        DpapiDataProtectionProvider provider = new DpapiDataProtectionProvider("Sample");

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private FavesrusUserManager _userManager;
        private IAuthenticationManager _authManager;
        private FavesrusRoleManager _roleManager;

        public ApiBaseController()
        {
            UserManager.UserTokenProvider = new DataProtectorTokenProvider<FavesrusUser>(provider.Create("EmailConfirmation"));
        }

        public ApiBaseController
            (FavesrusUserManager userManager, 
                FavesrusRoleManager roleManager, 
                IAuthenticationManager authManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            AuthManager = authManager;
            UserManager.UserTokenProvider = new DataProtectorTokenProvider<FavesrusUser>(provider.Create("EmailConfirmation"));
        }

        public ILog Log
        {
            get
            {
                return log;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public IAuthenticationManager AuthManager
        {
            get
            {
                if(_authManager == null)
                    AuthManager = HttpContext.Current.GetOwinContext().Authentication;
                return _authManager;
            }
            private set
            {
                _authManager = value;
            }
        }



        public FavesrusUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                    UserManager = HttpContext.Current.GetOwinContext().GetUserManager<FavesrusUserManager>();

                return _userManager;
            }
            private set
            {
                _userManager = value;
            }
        }

        public FavesrusRoleManager RoleManager
        {
            get
            {
                if (_roleManager == null)
                    RoleManager = HttpContext.Current.GetOwinContext().GetUserManager<FavesrusRoleManager>();
                return _roleManager;
            }
            private set
            {
                _roleManager = value;
            }
        }
    }
}
