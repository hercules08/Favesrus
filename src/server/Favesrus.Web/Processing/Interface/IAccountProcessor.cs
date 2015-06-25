using Favesrus.Data.Dtos;
using Favesrus.Data.RequestModels;
using Favesrus.Server.Dto.FavesrusUser;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FavesApi = Favesrus.Server.Controllers.WebApi;

namespace Favesrus.Server.Processing.Interface
{
    public interface IAccountProcessor
    {

        //IHttpActionResult 
        //    ForgotPassword(ForgotPasswordViewModel model, 
        //    HttpRequestMessage requestMessage,
        //    FavesApi.AccountController controller);

        //Task<IHttpActionResult> 
        //    ForgotPasswordAsync(ForgotPasswordViewModel model, 
        //    HttpRequestMessage requestMessage,
        //    FavesApi.AccountController controller);

        IHttpActionResult ResetPassword(string userId, string code, HttpRequestMessage requestMessage);
        Task<IHttpActionResult> ResetPasswordAsync(string userId, string code, HttpRequestMessage requestMessag);

        FavesrusUserModel RegisterUser(RegisterModel model);
        Task<FavesrusUserModel> RegisterUserAsync(RegisterModel model);
        
        FavesrusUserModel LoginUser(LoginModel model);
        Task<FavesrusUserModel> LoginUserAsync(LoginModel model);

        //IHttpActionResult 
        //    LoginFacebook(LoginFacebookModel model, 
        //    HttpRequestMessage requestMessage,
        //    FavesApi.AccountController controller);
        //Task<IHttpActionResult> 
        //    LoginFacebookAsync(LoginFacebookModel model, 
        //    HttpRequestMessage requestMessage,
        //    FavesApi.AccountController controller);
    }
}
