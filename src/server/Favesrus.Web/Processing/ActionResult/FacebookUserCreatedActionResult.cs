using Favesrus.Core.Common;
using Favesrus.Data.Dtos;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.Server.Processing.ActionResult
{
    public class FacebookUserCreatedActionResult:IHttpActionResult
    {
        private readonly FavesrusUserModel _user;
        private readonly HttpRequestMessage _requestMessage;

        public FacebookUserCreatedActionResult(HttpRequestMessage requestMessage,
            FavesrusUserModel user)
        {
            _requestMessage = requestMessage;
            _user = user;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var responseObject = ResponseFactory.CreateEntityResponseModel(_user);
            var responseMessage = _requestMessage.CreateResponse(HttpStatusCode.Created, responseObject);
            //responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_user);

            return responseMessage;
        }
    }
}