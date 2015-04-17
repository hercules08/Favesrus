using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Processing
{
    public class ResponseModel
    {
        public IEnumerable<object> Items { get; set; }
        public object Entity { get; set; }
        
    }
}