using Favesrus.Core.Logging;
using Favesrus.Core.TypeMapping;
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

        public BaseService(ILogManager logManager = null, IAutoMapper mapper = null)
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
    }
}
