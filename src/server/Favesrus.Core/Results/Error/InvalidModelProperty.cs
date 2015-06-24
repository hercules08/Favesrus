
namespace Favesrus.Core.Results.Error
{
    public class InvalidModelProperty
    {
        public InvalidModelProperty()
        {

        }

        public InvalidModelProperty(string errorItem, string reason)
        {
            ErrorItem = errorItem;
            Reason = reason;
        }

        public string ErrorItem { get; set; }
        public string Reason { get; set; }
    }
}
