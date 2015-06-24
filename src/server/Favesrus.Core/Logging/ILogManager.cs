using NLog.Interface;

namespace Favesrus.Core.Logging
{
    public interface ILogManager
    {
        ILogger GetLogger();
    }
}
