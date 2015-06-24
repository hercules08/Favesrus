using Favesrus.Core.Logging;
using NLog.Interface;
using System.Web.Http;

namespace Favesrus.API.Controllers
{
    public class BaseApiController:ApiController
    {
        ILogger _logger;

        public BaseApiController(ILogManager logManager = null)
        {
            Logger = logManager.GetLogger();
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
    }
}
