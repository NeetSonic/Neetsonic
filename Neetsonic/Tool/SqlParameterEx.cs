using System;

namespace Neetsonic.Tool
{
    /// <summary>
    /// SqlParameter扩展方法
    /// </summary>
    public static class SqlParameterEx
    {
        /// <summary>
        /// 可以为空的数据库字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>数据库字符串</returns>
        public static object NullableString(string str)
        {
            if(str is null)
            {
                return DBNull.Value;
            }
            return str;
        }
    }
}