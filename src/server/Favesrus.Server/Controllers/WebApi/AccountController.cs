using Favesrus.Model.Entity;
using Favesrus.Server.Dto.FavesrusUser;
using Favesrus.Server.ErrorHandling;
using Favesrus.Server.Exceptions;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Models;
using Favesrus.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Faves = Favesrus.Common;
using System.Net.Mail;
using System.Web;
using Favesrus.Server.Filters;
using Favesrus.Server.Processing.Interface;
using Favesrus.Server.Processing.ActionResult;

namespace Favesrus.Server.Controllers.WebApi
{
    [RoutePrefix("api/account")]
    public partial class AccountController : ApiBaseController
    {
        private IAutoMapper _mapper;
        private IFavesrusUserProcessor _favesrusUserProcessor;

        //public AccountController()
        //    :base()
        //{

        //}

        public AccountController(IAutoMapper mapper,
            IFavesrusUserProcessor favesrusUserProcessor)
        {
            _mapper = mapper;
            _favesrusUserProcessor = favesrusUserProcessor;
        }

        public AccountController
            (IAutoMapper mapper,
                IFavesrusUserProcessor favesrusUserProcessor,
                FavesrusUserManager userManager,
                FavesrusRoleManager roleManager,
                IAuthenticationManager authManager)
            : base(userManager, roleManager, authManager)
        {
            _mapper = mapper;
            _favesrusUserProcessor = favesrusUserProcessor;
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
