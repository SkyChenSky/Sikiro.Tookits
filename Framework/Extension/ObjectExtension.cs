namespace Framework.Common.Extension
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 对象是空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 对象不为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static string ToStr(this object input)
        {
            return input.IsNull() ? null : input.ToString();
        }
    }
}
