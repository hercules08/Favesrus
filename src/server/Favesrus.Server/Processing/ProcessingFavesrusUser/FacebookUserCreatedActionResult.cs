using Favesrus.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Favesrus.Server.Processing.ProcessingFavesrusUser
{
    public class FacebookUserCreatedActionResult:IHttpActionResult
    {
        private readonly FavesrusUser _user;
        private readonly HttpRequestMessage _requestMessage;

        public FacebookUserCreatedActionResult(HttpRequestMessage requestMessage,
            FavesrusUser user)
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
            var responseMessage = _requestMessage.CreateResponse(HttpStatusCode.Created, _user);
            
            return responseMessage;
        }
    }
}