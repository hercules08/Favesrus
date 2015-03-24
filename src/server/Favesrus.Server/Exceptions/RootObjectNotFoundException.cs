using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Exceptions
{
    public class RootObjectNotFoundException:Exception
    {
        public RootObjectNotFoundException(string message) : base(message)
        {

        }
    }
}