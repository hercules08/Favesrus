using Faves = Favesrus.Common;
using Favesrus.Model.Entity;
using Favesrus.Server.Dto.FavesrusUser;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.Owin.Security.Cookies;
using System.Collections.Generic;
using System.Net;

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

        [HttpPost]
        [Route("registerfacebook")]
        public async Task<IHttpActionResult> RegisterFacebook(RegisterFacebookModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserLoginInfo loginInfo =
                new UserLoginInfo(Faves.Constants.FACEBOOK_PROVIDER,
                        model.ProviderKey);

            FavesrusUser user = await UserManager.FindAsync(loginInfo);

            if (user == null)
            {
                user = mapper.Map<FavesrusUser>(model);
                user.UserName = model.Email;

                return await RegisterUser(user,loginInfo,null);
            }
            else
            {
                return BadRequest("The user already exists!");
            }
        }

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

        private async Task<IHttpActionResult> RegisterUser(FavesrusUser user, UserLoginInfo loginInfo = null ,string password = null)
        {
            IdentityResult result;
            IHttpActionResult errorResult;
            
            if(string.IsNullOrEmpty(password))
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

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserLoginInfo loginInfo = new UserLoginInfo(Faves.Constants.FACEBOOK_PROVIDER,
            model.ProviderKey);

            FavesrusUser user = await UserManager.FindAsync(loginInfo);

            if (user != null)
            {
                ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthManager.SignOut();
                AuthManager.SignIn(
                    new AuthenticationProperties
                        { IsPersistent = true}, ident);

                var dtoFavesUser = mapper.Map<DtoFavesrusUser>(user);

                return Ok(dtoFavesUser);
            }
            else
            {
                RegisterFacebookModel registerModel = new RegisterFacebookModel();
                registerModel.Email = model.Email;
                registerModel.ProviderKey = model.ProviderKey;

                return await RegisterFacebook(registerModel);
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
    }
}
