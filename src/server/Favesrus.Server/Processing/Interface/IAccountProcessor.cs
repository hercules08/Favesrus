using FavesApi = Favesrus.Server.Controllers.WebApi;
using Favesrus.Server.Dto.FavesrusUser;
using Favesrus.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Favesrus.Server.Controllers;

namespace Favesrus.Server.Processing.Interface
{
    public interface IAccountProcessor
    {

        IHttpActionResult 
            ForgotPassword(ForgotPasswordViewModel model, 
            HttpRequestMessage requestMessage,
            FavesApi.AccountController controller);

        Task<IHttpActionResult> 
            ForgotPasswordAsync(ForgotPasswordViewModel model, 
            HttpRequestMessage requestMessage,
            FavesApi.AccountController controller);

        IHttpActionResult ResetPassword(string userId, string code, HttpRequestMessage requestMessage);
        Task<IHttpActionResult> ResetPasswordAsync(string userId, string code, HttpRequestMessage requestMessag);

        DtoFavesrusUser RegisterUser(RegisterModel model);
        Task<DtoFavesrusUser> RegisterUserAsync(RegisterModel model);
        
        DtoFavesrusUser LoginUser(LoginModel model);
        Task<DtoFavesrusUser> LoginUserAsync(LoginModel model);

        IHttpActionResult 
            LoginFacebook(LoginFacebookModel model, 
            HttpRequestMessage requestMessage,
            FavesApi.AccountController controller);
        Task<IHttpActionResult> 
            LoginFacebookAsync(LoginFacebookModel model, 
            HttpRequestMessage requestMessage,
            FavesApi.AccountController controller);
    }
}
