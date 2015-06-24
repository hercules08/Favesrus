using System.Collections.Generic;

namespace Favesrus.Core.Results.Base
{
    public class ResponseFactory
    {
        public static ResponseObject CreateResponseObject(string status, string message, ResponseModel model, bool hasItems)
        {
            return new ResponseObject(status, message, model, hasItems);
        }

        public static ResponseModel CreateItemsResponseModel(IEnumerable<object> items)
        {
            return new ResponseModel { Items = items };
        }

        public static ResponseModel CreateEntityResponseModel(object entity)
        {
            return new ResponseModel { Entity = entity };
        }
    }
}
