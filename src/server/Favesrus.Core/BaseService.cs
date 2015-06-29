using Favesrus.Core.Logging;
using Favesrus.Core.Results.Error;
using Favesrus.Core.TypeMapping;
using Microsoft.AspNet.Identity;
using NLog.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Core
{
    public class BaseService
    {
        ILogger _logger;
        IAutoMapper _mapper;

        public BaseService(ILogManager logManager, IAutoMapper mapper)
        {
            Logger = logManager.GetLogger();
            Mapper = mapper;
        }

        public ILogger Logger
        {
            get
            {
                return _logger ?? new LogManager().GetLogger();
            }
            private set
            {
                _logger = value;
            }
        }

        public IAutoMapper Mapper
        {
            get
            {
                return _mapper ?? new AutoMapperAdapter();
            }
            private set
            {
                _mapper = value;
            }
        }

        public List<InvalidModelProperty> GetErrorsFromIdentityResult(IdentityResult result)
        {
            List<InvalidModelProperty> invalidModelStates = new List<InvalidModelProperty>();
            int i = 1;

            foreach (string error in result.Errors)
            {
                InvalidModelProperty invalidItem = new InvalidModelProperty();

                invalidItem.ErrorItem = "issue" + i;
                invalidItem.Reason = error;
                invalidModelStates.Add(invalidItem);
                i++;
            }

            return invalidModelStates;
        }
    }
}
