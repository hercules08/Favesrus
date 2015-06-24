using Favesrus.Data.Dtos;
using Favesrus.Server.Dto.FavesrusUser;
using Favesrus.Server.Filters;
using Favesrus.Server.Processing.ActionResult;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.Server.Controllers.WebApi
{
    public partial class AccountController : ApiBaseController
    {
        [HttpPost]
        [Route("register")]
        [ValidateModel]
        public async Task<IHttpActionResult> Register(HttpRequestMessage requestMessage, RegisterModel model)
        {
            string successMessage = "Successfully registered Faves 'R' Us user.";

            Log.Info(string.Format("Attempt register as {0} with password {1}", model.Email, model.Password));

            FavesrusUserModel dtoFavesrusUser = await _accountProcessor.RegisterUserAsync(model);

            var result = new RegisterDtoFavesrusActionResult(
                requestMessage,
                dtoFavesrusUser,
                successMessage);

            return result;
        } 
    }
}
