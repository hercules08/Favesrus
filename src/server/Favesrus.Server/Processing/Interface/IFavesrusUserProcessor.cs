using Favesrus.Server.Controllers.WebApi;
using Favesrus.Server.Dto.FavesrusUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.Server.Processing.Interface
{
    public interface IFavesrusUserProcessor
    {
        DtoFavesrusUser RegisterUser(RegisterModel model);
        Task<DtoFavesrusUser> RegisterUserAsync(RegisterModel model);
        DtoFavesrusUser LoginUser(LoginModel model);
        Task<DtoFavesrusUser> LoginUserAsync(LoginModel model);


        Task<IHttpActionResult> LoginFacebookAsync(LoginFacebookModel model, HttpRequestMessage requestMessage, AccountController controller);
        IHttpActionResult LoginFacebook(LoginFacebookModel model, HttpRequestMessage requestMessage, AccountController controller);
    }
}
