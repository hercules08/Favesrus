using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Exceptions
{
    public class InvalidModelStateException:Exception
    {
        public InvalidModelStateException(string message):base(message)
        {

        }
    }
}