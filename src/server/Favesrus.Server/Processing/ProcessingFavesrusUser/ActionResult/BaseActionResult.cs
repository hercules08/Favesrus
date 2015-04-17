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
    public class BaseActionResult<T> : IHttpActionResult
    {
        private readonly T _entity;
        private readonly HttpRequestMessage _requestMessage;
        private string _message;
        private string _status;
        private HttpStatusCode _responseStatus;

        public BaseActionResult(
            HttpRequestMessage requestMessage,
            T entity,
            string message,
            string status,
            HttpStatusCode responseStatus = HttpStatusCode.OK)
        {
            _requestMessage = requestMessage;
            _entity = entity;
            _message = message;
            _status = status;
            _responseStatus = responseStatus;
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

            if (localEntity != null)
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

            var responseObject = ResponseFactory
                .CreateResponseObject(_status, _message, responseModel, hasItems);
            var responseMessage = _requestMessage.CreateResponse(_responseStatus, responseObject);
            //responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_dtoFavesrusUser);

            return responseMessage;
        }
    }
}