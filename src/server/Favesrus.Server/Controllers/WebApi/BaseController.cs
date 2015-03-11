using Favesrus.Services;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;


namespace Favesrus.Server.Controllers.WebApi
{
    public abstract class BaseController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private FavesrusUserManager userManager;
        private IAuthenticationManager authManager;
        private FavesrusRoleManager roleManager;

        public BaseController()
        {

        }

        public BaseController
            (FavesrusUserManager userManager, 
                FavesrusRoleManager roleManager, 
                IAuthenticationManager authManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            AuthManager = authManager;
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

        public IAuthenticationManager AuthManager
        {
            get
            {
                return authManager ?? 
                    HttpContext.Current.GetOwinContext()
                    .Authentication;
            }
            private set
            {
                authManager = value;
            }
        }

        public FavesrusUserManager UserManager
        {
            get
            {
                return userManager ?? 
                    HttpContext.Current.GetOwinContext()
                    .GetUserManager<FavesrusUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }

        public FavesrusRoleManager RoleManager
        {
            get
            {
                return roleManager ?? 
                    HttpContext.Current.GetOwinContext()
                    .GetUserManager<FavesrusRoleManager>();
            }
            private set
            {
                roleManager = value;
            }
        }
    }
}
