using System;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Common.Extension
{
    public static class EncryptExtension
    {
        /// <summary> 
        /// 将字符串使用base64算法编码 
        /// </summary> 
        /// <param name="encodingName">编码类型（编码名称） 
        /// * 代码页 名称 
        /// * 1200 "UTF-16LE"、"utf-16"、"ucs-2"、"unicode"或"ISO-10646-UCS-2" 
        /// * 1201 "UTF-16BE"或"unicodeFFFE" 
        /// * 1252 "windows-1252"
        /// * 65000 "utf-7"、"csUnicode11UTF7"、"unicode-1-1-utf-7"、"unicode-2-0-utf-7"、"x-unicode-1-1-utf-7"或"x-unicode-2-0-utf-7" 
        /// * 65001 "utf-8"、"unicode-1-1-utf-8"、"unicode-2-0-utf-8"、"x-unicode-1-1-utf-8"或"x-unicode-2-0-utf-8" 
        /// * 20127 "us-ascii"、"us"、"ascii"、"ANSI_X3.4-1968"、"ANSI_X3.4-1986"、"cp367"、"csASCII"、"IBM367"、"iso-ir-6"、"ISO646-US"或"ISO_646.irv:1991" 
        /// * 54936 "GB18030"
        /// </param>
        /// <param name="source">待加密的字符串</param>
        /// <returns>加密后的字符串</returns> 
        public static string EncodeBase64String(this string source, string encodingName = "UTF-8")
        {
            byte[] bytes = Encoding.GetEncoding(encodingName).GetBytes(source);
            return Convert.ToBase64String(bytes);
        }

        /// <summary> 
        /// 将字符串使用base64算法解码
        /// </summary> 
        /// <param name="encodingName">编码类型</param> 
        /// <param name="base64String">已用base64算法加密的字符串</param> 
        /// <returns>解密后的字符串</returns> 
        public static string DecodeBase64String(this string base64String, string encodingName = "UTF-8")
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64String);
                return Encoding.GetEncoding(encodingName).GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 将字符串使用MD5算法解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncodeMd5String(this string input)
        {
            if (input.IsNullOrEmpty())
                return input;

            var md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.Default.GetBytes(input));
            return BitConverter.ToString(result).Replace("-", "");
        }

    }
}
