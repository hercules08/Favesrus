using System.Collections.Generic;

namespace Favesrus.Core.Results.Base
{
    public class ResponseModel
    {
        public IEnumerable<object> Items { get; set; }
        public object Entity { get; set; }
    }
}
