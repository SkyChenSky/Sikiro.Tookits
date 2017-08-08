using System.ComponentModel.DataAnnotations;

namespace Framework.Common.Attribute
{
    ///<summary>
    /// 手机号码
    /// </summary>
    public class PhoneNumAttribute : RegularExpressionAttribute
    {
        private const string RegexPattern = @"^1[0-9]{10}$";
        public PhoneNumAttribute(): base(RegexPattern)
        {
            ErrorMessage = "手机号码不正确";
        }
    }
}
