using System.Collections.Specialized;
using System.Net;
using System.Reflection;

namespace Neetsonic.Tool.Extensions
{
    /// <summary>
    /// WebHeaderCollection扩展
    /// </summary>
    public static class WebHeaderCollectionEx
    {
        /// <summary>
        /// 设置某些特殊值用到的
        /// </summary>
        /// <param name="header"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetHeaderValue(this WebHeaderCollection header, string name, string value)
        {
            PropertyInfo property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if(property != null)
            {
                if(property.GetValue(header, null) is NameValueCollection collection)
                {
                    collection[name] = value;
                }
            }
        }
    }
}