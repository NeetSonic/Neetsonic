using System;

namespace Neetsonic.Tool.Extensions
{
    /// <summary>
    ///  DateTime扩展类
    /// </summary>
    public static class DateTimeEx
    {
        /// <summary>
        /// 获取从UTC 1970年以来经过的毫秒数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long Utc1970InMillisec(this DateTime date) =>(date.Ticks - 621355968000000000L) / 10000;
    }
}