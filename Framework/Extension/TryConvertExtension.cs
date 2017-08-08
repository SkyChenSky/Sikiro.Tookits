using System;

namespace Framework.Common.Extension
{
    public static class TryConvertExtension
    {
        #region 类型转换
        /// <summary>
        /// string转int
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="defaultNum">转换失败默认</param>
        /// <returns></returns>
        public static int TryInt(this string inputStr, int defaultNum = 0)
        {
            int num;
            return int.TryParse(inputStr, out num) ? num : defaultNum;
        }

        /// <summary>
        /// string转long
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="defaultNum">转换失败默认</param>
        /// <returns></returns>
        public static long TryLong(this string inputStr, long defaultNum = 0)
        {
            long num;
            return long.TryParse(inputStr, out num) ? num : defaultNum;
        }

        /// <summary>
        /// string转double
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="defaultNum">转换失败默认值</param>
        /// <returns></returns>
        public static double TryDouble(this string inputStr, double defaultNum = 0)
        {
            double num;
            return double.TryParse(inputStr, out num) ? num : defaultNum;
        }

        /// <summary>
        /// string转decimal
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="defaultNum">转换失败默认值</param>
        /// <returns></returns>
        public static decimal TryDecimal(this string inputStr, decimal defaultNum = 0)
        {
            decimal num;
            return decimal.TryParse(inputStr, out num) ? num : defaultNum;
        }

        /// <summary>
        /// string转decimal
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="defaultNum">转换失败默认值</param>
        /// <returns></returns>
        public static float TryFloat(this string inputStr, float defaultNum = 0)
        {
            float num;
            return float.TryParse(inputStr, out num) ? num : defaultNum;
        }

        /// <summary>
        /// string转bool
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="defaultBool">转换失败默认值</param>
        /// <returns></returns>
        public static bool TryBool(this string inputStr, bool defaultBool = false)
        {
            bool output;
            return bool.TryParse(inputStr, out output) ? output : defaultBool;
        }

        /// <summary>
        /// 值类型转string
        /// </summary>
        /// <param name="inputObj">输入</param>
        /// <param name="defaultStr">转换失败默认值</param>
        /// <returns></returns>
        public static string TryString(this ValueType inputObj, string defaultStr = "")
        {
            var output = inputObj.IsNull() ? defaultStr : inputObj.ToString();
            return output;
        }

        /// <summary>
        /// 字符串转时间
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime TryDateTime(this string inputStr, DateTime defaultValue = default(DateTime))
        {
            if (inputStr.IsNullOrEmpty())
                return defaultValue;

            DateTime outPutDateTime;
            return DateTime.TryParse(inputStr, out outPutDateTime) ? outPutDateTime : defaultValue;
        }

        /// <summary>
        /// 字符串去空格
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <returns></returns>
        public static string TryTrim(this string inputStr)
        {
            var output = inputStr.IsNullOrEmpty() ? inputStr : inputStr.Trim();
            return output;
        }

        /// <summary>
        /// 字符串转枚举
        /// </summary>
        /// <typeparam name="T">输入</typeparam>
        /// <param name="str"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T TryEnum<T>(this string str, T t = default(T)) where T : struct
        {
            T result;
            return Enum.TryParse(str, out result) ? result : t;
        }
        #endregion
    }
}
