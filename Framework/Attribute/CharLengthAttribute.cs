using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Framework.Attribute
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    public class CharLengthAttribute : ValidationAttribute
    {
        public long Maxlength { get; private set; }

        public CharLengthAttribute(long maxlength):base("asdasdasd{0}")
        {
            Maxlength = maxlength;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var contentLenght = Encoding.Default.GetBytes(value.ToString()).Length;
            return contentLenght <= Maxlength;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? string.Format("The field {0} must be a string with a maximum length of {1}.",
                    name, Maxlength)
                : string.Format(ErrorMessage, name, Maxlength);
        }
    }
}
