using Favesrus.Model.Entity;
using Favesrus.Server.Dto.FavesrusUser;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Favesrus.Server.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "/")
        {
            //if(!HttpContext.User.Identity.IsAuthenticated)
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                FavesrusUser user = await UserManager.FindAsync(model.Email, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password.");
                    Danger("Invalid name or password.");
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = true }, ident);
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }

            }
            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }
	}
}