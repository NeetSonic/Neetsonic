using System;

namespace Neetsonic.Tool
{
    /// <summary>
    /// 时间工具
    /// Neetsonic
    /// 2018.5.17
    /// </summary>
    public static class TimeTool
    {
        /// <summary>
        /// 返回当前时间戳
        /// </summary>
        /// <returns>当前时间戳</returns>
        public static long NowUnixStamp()=> DateTimeToUnixStamp(DateTime.Now);
        /// <summary>
        /// Datatime类型转Unix时间戳
        /// </summary>
        /// <param name="time">时间对象</param>
        /// <returns>对应的时间戳</returns>
        public static long DateTimeToUnixStamp(DateTime time) => (time.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        /// <summary>
        /// Unix时间戳转Datatime类型
        /// </summary>
        /// <param name="stamp">时间戳</param>
        /// <returns>时间对象</returns>
        public static DateTime UnixStampToDateTime(long stamp) => TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).AddSeconds(double.Parse(stamp.ToString()));
    }
}