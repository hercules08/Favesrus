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
    public class ErrorActionResult : IHttpActionResult
    {
        private readonly object _entity;
        private readonly HttpRequestMessage _requestMessage;
        private readonly string _status;
        private readonly string _statusDetail;

        public ErrorActionResult(
            HttpRequestMessage requestMessage,
            string status,
            string statusDetail,
            object entity = null)
        {
            _requestMessage = requestMessage;
            _entity = entity;
            _status = status;
            _statusDetail = statusDetail;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            ResponseModel responseModel;

            IEnumerable<object> localEntity = _entity as IEnumerable<object>;
            bool hasItems = false;

            if(localEntity != null)
            {
                if ((localEntity as IEnumerable<object>).Count() > 1)
                {
                    responseModel = ResponseFactory.CreateItemsResponseModel(localEntity);
                    hasItems = true;
                }
                else
                {
                    responseModel = ResponseFactory.CreateEntityResponseModel(localEntity.ElementAtOrDefault(0));
                }
            }
            else
            {
                responseModel = ResponseFactory.CreateEntityResponseModel(_entity);
            }

            var responseObject = ResponseFactory.CreateResponseObject(_status, _statusDetail, responseModel, hasItems);
            var responseMessage = _requestMessage.CreateResponse(HttpStatusCode.BadRequest, responseObject);

            return responseMessage;
        }

    }
}