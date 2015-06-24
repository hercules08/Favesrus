using NLog.Interface;

namespace Favesrus.Core.Logging
{
    public class LogManager : ILogManager
    {
        /// <summary>
        /// Gets the logger for the current class
        /// </summary>
        /// <returns>Logger</returns>
        public ILogger GetLogger()
        {
            return new LoggerAdapter(NLog.LogManager.GetCurrentClassLogger());
        }
    }
}
