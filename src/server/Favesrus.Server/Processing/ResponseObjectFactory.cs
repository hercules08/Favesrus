using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Processing
{
    public class ResponseObjectFactory
    {
        public static ResponseObject CreateResponseObject(string status, ResponseModel model)
        {
            return new ResponseObject(status, model);
        }

        public static ResponseModel CreateMessageResponseModel(string message)
        {
            return new ResponseModel { Message = message };
        }

        public static ResponseModel CreateItemsResponseModel(IEnumerable<object> items, string message)
        {
            return new ResponseModel { Message = message, Items = items };
        }

        public static ResponseModel CreateEntityResponseModel(object entity, string message)
        {
            return new ResponseModel { Message = message, Entity = entity };
        }

        public static ResponseModel CreateFullResponseModel(ICollection<object> items, object entity, string message)
        {
            return new ResponseModel { Items = items, Entity = entity, Message = message };
        }
    }
}