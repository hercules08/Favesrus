using Favesrus.Common;
using Favesrus.Model.Entity;
using Favesrus.Server.Dto;
using Favesrus.Server.Dto.FavesrusUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Favesrus.Server.Processing.ProcessingFavesrusUser.ActionResult
{
    public class FacebookUserCreatedActionResult:IHttpActionResult
    {
        private readonly DtoFavesrusUser _user;
        private readonly HttpRequestMessage _requestMessage;

        public FacebookUserCreatedActionResult(HttpRequestMessage requestMessage,
            DtoFavesrusUser user)
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
            var responseObject = ResponseObjectFactory.CreateEntityResponseModel(_user, Constants.Status.FACEBOOK_USER_CREATED);
            var responseMessage = _requestMessage.CreateResponse(HttpStatusCode.Created, responseObject);
            responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_user);

            return responseMessage;
        }
    }
}