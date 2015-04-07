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

        public BaseActionResult(
            HttpRequestMessage requestMessage,
            T entity,
            string message,
            string status)
        {
            _requestMessage = requestMessage;
            _entity = entity;
            _message = message;
            _status = status;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            ResponseModel responseModel;

            IEnumerable<object> localEntity = _entity as IEnumerable<object>;

            if (localEntity != null)
            {
                if ((localEntity as IEnumerable<object>).Count() > 1)
                {
                    responseModel = ResponseFactory.CreateItemsResponseModel(localEntity, _message);
                }
                else
                {
                    responseModel = ResponseFactory.CreateEntityResponseModel(localEntity.ElementAtOrDefault(0), _message);
                }
            }
            else
            {
                responseModel = ResponseFactory.CreateEntityResponseModel(_entity, _message);
            }

            var responseObject = ResponseFactory
                .CreateResponseObject(_status, responseModel);
            var responseMessage = _requestMessage.CreateResponse(HttpStatusCode.OK, responseObject);
            //responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_dtoFavesrusUser);

            return responseMessage;
        }
    }
}