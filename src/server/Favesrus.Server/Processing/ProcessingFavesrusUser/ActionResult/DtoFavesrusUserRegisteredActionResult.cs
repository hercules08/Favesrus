﻿using Favesrus.Common;
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
        private readonly string _message;

        public DtoFavesrusUserRegisteredActionResult(HttpRequestMessage requestMessage, DtoFavesrusUser dtoFavesrusUser, string message)
        {
            _requestMessage = requestMessage;
            _dtoFavesrusUser = dtoFavesrusUser;
            _message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var responseModel = 
                ResponseFactory.CreateEntityResponseModel(_dtoFavesrusUser, _message);
            var responseObject = ResponseFactory
                .CreateResponseObject(Constants.Status.FAVES_USER_REGISTERED, responseModel);
            var responseMessage = _requestMessage.CreateResponse(HttpStatusCode.Created, responseObject);
            //responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_dtoFavesrusUser);

            return responseMessage;
        }
    }
}