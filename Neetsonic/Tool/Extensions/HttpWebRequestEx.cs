using System.Collections.Specialized;
using System.Net;
using System.Reflection;

namespace Neetsonic.Tool.Extensions
{
    /// <summary>
    /// Http请求扩展类
    /// </summary>
    public static class HttpWebRequestEx
    {
        /// <summary>
        /// 设置标头（针对Connection等无法直接设置的）
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <param name="name">标头名字</param>
        /// <param name="value">标头值</param>
        public static void SetHeaderValue(this HttpWebRequest request, string name, string value)
        {
            PropertyInfo property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if(property != null)
            {
                if(property.GetValue(request.Headers, null) is NameValueCollection collection)
                    collection[name] = value;
            }
        }
    }
}