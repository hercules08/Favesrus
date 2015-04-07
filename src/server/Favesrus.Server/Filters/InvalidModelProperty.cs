using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Favesrus.Server.Filters
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
