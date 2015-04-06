using Favesrus.Common;
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
    public class DtoFavesrusUserRegisteredActionResult:IHttpActionResult
    {
        private readonly DtoFavesrusUser _dtoFavesrusUser;
        private readonly HttpRequestMessage _requestMessage;

        public DtoFavesrusUserRegisteredActionResult(HttpRequestMessage requestMessage, DtoFavesrusUser dtoFavesrusUser)
        {
            _requestMessage = requestMessage;
            _dtoFavesrusUser = dtoFavesrusUser;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var responseObject = 
                ResponseObjectFactory
                .CreateEntityResponseModel(
                _dtoFavesrusUser, 
                Constants.Status.FAVES_USER_REGISTERED);
            var responseMessage = _requestMessage.CreateResponse(HttpStatusCode.Created, responseObject);
            responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_dtoFavesrusUser);

            return responseMessage;
        }
    }
}