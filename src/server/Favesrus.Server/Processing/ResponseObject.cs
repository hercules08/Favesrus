using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Processing
{
    public class ResponseObject
    {
        public string Status { get; private set; }
        public ResponseModel Model { get; private set; }

        public ResponseObject(string status, ResponseModel model)
        {
            Status = status;
            Model = model;
        }
    }
}