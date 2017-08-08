using System.ComponentModel.DataAnnotations;

namespace Framework.Common.Attribute
{
    ///<summary>
    /// 邮箱验证特性
    /// </summary>
    public class IdCardAttribute : RegularExpressionAttribute
    {
        private const string RegexPattern = @"^[1-9]\d{16}[\dXx]$";
        public IdCardAttribute(): base(RegexPattern)
        {
            ErrorMessage = "身份证格式不正确";
        }
    }
}
