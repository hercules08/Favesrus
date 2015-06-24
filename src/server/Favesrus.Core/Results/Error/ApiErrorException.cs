using System;
using System.Net;

namespace Favesrus.Core.Results.Error
{
    public class ApiErrorException:Exception
    {
        private readonly object _entity;
        private readonly string _apiStatus;
        private readonly HttpStatusCode _statusCode;
    
        public ApiErrorException(string message, object entity = null, HttpStatusCode badRequest = HttpStatusCode.BadRequest)
            :base(message)
        {
            _statusCode = badRequest;
            _entity = entity;
        }

        public object Entity
        {
            get
            {
                return _entity;
            }
        }

        public string ApiStatus
        {
            get
            {
                return _apiStatus;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return _statusCode;
            }
        }
    }
}
