using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Exceptions
{
    public class RootObjectNotFoundException:BaseErrorException
    {
        public RootObjectNotFoundException(string message, object entity = null) : base(message)
        {

        }
    }
}