using Favesrus.Core.Logging;
using Favesrus.Core.TypeMapping;
using NLog.Interface;
using System.Web.Http;

namespace Favesrus.Core
{
    public class BaseApiController:ApiController
    {
        ILogger _logger;
        IAutoMapper _mapper;

        public BaseApiController(ILogManager logManager, IAutoMapper mapper)
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
