﻿using Favesrus.Server.Exceptions;
using Favesrus.Server.Filters;
using Favesrus.Server.Models;
using Favesrus.Server.Processing;
using Favesrus.Services;
using Microsoft.AspNet.Identity;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.Server.Controllers.WebApi
{
    public partial class AccountController : ApiBaseController
    {
        [HttpPost]
        [AllowAnonymous]
        [ValidateModel]
        public async Task<IHttpActionResult> ForgotPassword(
            HttpRequestMessage requestMessage, 
            ForgotPasswordViewModel model)
        {
            return await _accountProcessor.ForgotPasswordAsync(model, requestMessage, this);
        }

        [HttpGet]
        [Route("resetpassword", Name = "ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(string userId, string code)
        {
            return await _accountProcessor.ResetPasswordAsync(userId, code, Request);
        }
    }
}
