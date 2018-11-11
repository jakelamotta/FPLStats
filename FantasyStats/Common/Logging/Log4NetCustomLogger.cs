using log4net;
using log4net.Config;
using System;

namespace Common.Logging
{
    public class Log4NetCustomLogger : ICustomLogger
    {
        private readonly ILog _logger;

        public Log4NetCustomLogger()
        {
            if (!LogManager.GetRepository().Configured)
            {
                XmlConfigurator.Configure();
            }

            _logger = LogManager.GetLogger(typeof(Log4NetCustomLogger));
        }

        public void Error(Exception e, string extendedInformation = "")
        {
            _logger.Error(extendedInformation, e);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }
    }
}
