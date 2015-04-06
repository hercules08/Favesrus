using Favesrus.Common;
using Favesrus.Server.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Favesrus.Server.Processing
{
    public class InvalidModelStateActionResult:IHttpActionResult
    {
        private readonly List<InvalidModelProperty> _items;
        private readonly HttpRequestMessage _requestMessage;

        public InvalidModelStateActionResult(HttpRequestMessage requestMessage,
            List<InvalidModelProperty> items)
        {
            _requestMessage = requestMessage;
            _items = items;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var responseModel = ResponseObjectFactory.CreateItemsResponseModel(_items, "Bad model state.");
            var responseObject = ResponseObjectFactory.CreateResponseObject(Constants.Status.INVALID_MODELSTATE, responseModel);
            var responseMessage = _requestMessage.CreateResponse(HttpStatusCode.BadRequest, responseObject);

            return responseMessage;
        }
    }
}