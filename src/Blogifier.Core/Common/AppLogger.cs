using Microsoft.Extensions.Logging;

namespace Blogifier.Core.Common
{
    public class AppLogger
    {
        ILogger<object> _logger;

        public AppLogger(ILogger<object> logger)
        {
            _logger = logger;
        }

        public void LogWarning(string msg)
        {
            if(_logger != null && ApplicationSettings.EnableLogging)
            {
                _logger.LogWarning("[BLOGIFIER] " + msg);
            }
        }

        public void LogError(string msg)
        {
            if (_logger != null && ApplicationSettings.EnableLogging)
            {
                _logger.LogError("[BLOGIFIER] " + msg);
            }
        }
    }
}
