using System;
using System.Linq;
using Framework.Common.Extension;
using log4net;
using log4net.Appender;

namespace Framework.Common.Helper
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static class LoggerHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(LoggerHelper));

        #region 文本日志
        /// <summary>
        /// 文本日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dir"></param>
        public static void WriteToFile(string message, string dir = "")
        {
            WriteToFileSetting(() => Log.Info(message), "info", dir);
        }

        /// <summary>
        /// 文本日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="dir"></param>
        public static void WriteToFile(string message, Exception ex, string dir = "")
        {
            WriteToFileSetting(() => Log.Error(message, ex), "error", dir);
        }

        private static void WriteToFileSetting(Action action, string appendersName, string dir = "")
        {
            var appenders = LogManager.GetRepository().GetAppenders();
            var appender = appenders.FirstOrDefault(i => i.Name == appendersName) as RollingFileAppender;
            if (appender != null)
            {
                appender.File = (dir.IsNullOrEmpty()
                    ? "log4net/{0:yyyyMMdd}/"
                    : "log4net/{2}/{1}/").Format(DateTime.Now, dir, appendersName);
                appender.ActivateOptions();
                action();
            }
        }
        #endregion
    }
}
