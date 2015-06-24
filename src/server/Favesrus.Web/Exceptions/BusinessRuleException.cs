using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Exceptions
{
    public class BusinessRuleException:BaseErrorException
    {
        private readonly string _status = "";

        public BusinessRuleException(string status, string message, object entity = null)
            : base(message, entity)
        {
            _status = status;
        }

        public string Status
        {
            get
            {
                return _status;
            }
        }
    }
}