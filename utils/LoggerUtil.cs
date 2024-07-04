using NLog;
using NLog.Config;

namespace saucedemotests.utils
{
    public static class LoggerUtil
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void LoadConfig(){
            var nlogConfig = new XmlLoggingConfiguration("config/nlog.config");
            LogManager.Configuration = nlogConfig;
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Debug(string message)
        {
            logger.Debug(message);
        }
    }
}