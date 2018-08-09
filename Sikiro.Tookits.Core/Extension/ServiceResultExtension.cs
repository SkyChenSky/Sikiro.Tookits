using Sikiro.Tookits.Core.Base;

namespace Sikiro.Tookits.Core.Extension
{
    /// <summary>
    /// ServiceResult扩展类
    /// </summary>
    public static class ServiceResultExtension
    {
        /// <summary>
        /// ServiceResult转换ServiceResult<T/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static ServiceResult<T> Convert<T>(this ServiceResult sr) where T : class,new()
        {
            return new ServiceResult<T>(sr);
        }
    }
}
