using NLog;
using SiteMercado.Desafio.Utils.Interfaces;
using System;

namespace SiteMercado.Desafio.Utils.Services
{
    public class LogNLog : ILog
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public LogNLog(){}

        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(Exception exception)
        {
            logger.Error($"Message: {exception.Message} StackTrace: {exception.StackTrace}");
        }
    }
}
