using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Exceptions
{
    public abstract class BaseErrorException : Exception
    {
        private readonly object _entity;

        public BaseErrorException(string message, object entity = null)
            : base(message)
        {
            _entity = entity;
        }

        public object Entity
        {
            get
            {
                return _entity;
            }
        }

    }
}