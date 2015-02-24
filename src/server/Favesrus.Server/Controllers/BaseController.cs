using Favesrus.Server.Helpers;
using Favesrus.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Favesrus.Server.Controllers
{
    public abstract class BaseController : Controller
    {
        private FavesrusUserManager userManager;
        private IAuthenticationManager authManager;
        private FavesrusRoleManager roleManager;

        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey) ? (List<Alert>)TempData[Alert.TempDataKey] : new List<Alert>();
            alerts.Add(new Alert
                {
                    AlertStyle = alertStyle,
                    Message = message,
                    Dismissable = dismissable
                });

            TempData[Alert.TempDataKey] = alerts;
        }

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

        protected void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
                Danger(error);
            }
        }

        public IAuthenticationManager AuthManager
        {
            get
            {
                return authManager ??
                    HttpContext.GetOwinContext()
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
                    HttpContext.GetOwinContext()
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
                    HttpContext.GetOwinContext()
                    .GetUserManager<FavesrusRoleManager>();
            }
            private set
            {
                roleManager = value;
            }
        }
	}
}