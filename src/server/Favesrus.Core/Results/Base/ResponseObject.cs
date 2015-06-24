
namespace Favesrus.Core.Results.Base
{
    public class ResponseObject
    {
        public string Status { get; private set; }
        public ResponseModel Model { get; private set; }
        public string Message { get; private set; }
        public bool HasItems { get; set; }

        public ResponseObject(string status, string message)
        {
            Status = status;
            Message = message;
            HasItems = false;
        }

        public ResponseObject(string status, string message, ResponseModel model, bool hasItems)
        {
            Status = status;
            Model = model;
            Message = message;
            HasItems = hasItems;
        }
    }
}