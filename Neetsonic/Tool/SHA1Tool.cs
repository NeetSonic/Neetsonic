using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Neetsonic.Tool
{
    /// <summary>
    /// SHA1校验码生成工具
    /// Neetsonic
    /// 2017.8.12
    /// </summary>
    public class SHA1Tool
    {
        /// <summary>
        /// 输出指定文件的SHA1校验码
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件的SHA1校验码</returns>
        public static string EncryptFile(string filePath)
        {
            using(FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using(SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                {
                    return Convert.ToBase64String(sha1.ComputeHash(fs));
                }
            }
        }
        /// <summary>
        /// 输出指定UNICODE字符串的SHA1校验码
        /// </summary>
        /// <param name="unicodeString">UNICODE字符串</param>
        /// <returns>SHA1校验码</returns>
        public static string EncryptUnicode(string unicodeString)
        {
            using(SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                return Convert.ToBase64String(sha1.ComputeHash(Encoding.Unicode.GetBytes(unicodeString)));
            }
        }
    }

    /// <summary>
    /// 带有SHA1校验码的文件
    /// </summary>
    public sealed class FileSHA1
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// SHA1校验码
        /// </summary>
        public string SHA1 { get; set; }
    }
}