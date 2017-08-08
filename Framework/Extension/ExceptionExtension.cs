using System;
using Framework.Common.Helper;

namespace Framework.Common.Extension
{
    public static class ExceptionExtension
    {
        #region 获取最底层异常
        /// <summary>
        /// 获取最底层异常
        /// </summary>
        public static Exception GetInnestException(this Exception ex)
        {
            var innerException = ex.InnerException;
            var exception2 = ex;
            while (innerException != null)
            {
                exception2 = innerException;
                innerException = innerException.InnerException;
            }
            return exception2;
        }
        #endregion

        #region 异常文本日志

        public static void WriteToFile(this Exception ex, string message, string dir = "")
        {
            LoggerHelper.WriteToFile(message, ex, dir);
        }
        #endregion
    }
}
