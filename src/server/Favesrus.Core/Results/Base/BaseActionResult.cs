using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Favesrus.Core.Results.Base
{
    //public static class APIRequestMessage
    //{
    //    public static System.Net.Http.HttpRequestMessage As4_5HttpRequestMessage(this System.Web.HttpRequest)
    //    {

    //    }
    //}

    public abstract class BaseActionResult:IHttpActionResult
    {
        private readonly HttpRequestMessage _requestMessage;
        private string _message;
        private string _apiStatus;
        private HttpStatusCode _responseStatus;

        public BaseActionResult(
            string apiStatus,
            string message,
            HttpStatusCode responseStatus = HttpStatusCode.OK)
        {
            
            //var context = new HttpContextWrapper(HttpContext.Current);
            //HttpRequestBase request = context.Request;

            _requestMessage = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;

            _apiStatus = apiStatus;
            _message = message;
            _responseStatus = responseStatus;
        }

        public string ApiStatus
        {
            get
            {
                return _apiStatus;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            var responseObject = ResponseFactory.CreateResponseObject(_apiStatus, _message, null, false);
            var responseMessage = _requestMessage.CreateResponse(_responseStatus, responseObject);
            //responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_dtoFavesrusUser);

            return responseMessage;
        }
    }

    public abstract class BaseActionResult<T>: IHttpActionResult
    {
        private readonly T _entity;
        private readonly HttpRequestMessage _requestMessage;
        private string _message;
        private string _apiStatus;
        private HttpStatusCode _responseStatus;

        public BaseActionResult(
            string apiStatus,
            string message,
            T entity,
            HttpStatusCode responseStatus = HttpStatusCode.OK)
        {
            _requestMessage = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;

            _entity = entity;
            _message = message;
            _apiStatus = apiStatus;
            _responseStatus = responseStatus;
        }

        public T Entity
        {
            get
            {
                return _entity;
            }
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
                .CreateResponseObject(_apiStatus, _message, responseModel, hasItems);
            var responseMessage = _requestMessage.CreateResponse(_responseStatus, responseObject);
            //responseMessage.Headers.Location = LocationLinkCalculator.GetLocationLink(_dtoFavesrusUser);

            return responseMessage;
        }
    }
}
