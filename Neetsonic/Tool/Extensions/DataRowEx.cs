using System;
using System.Data;

namespace Neetsonic.Tool.Extensions
{
    /// <summary>
    /// DataRow扩展
    /// </summary>
    public static class DataRowEx
    {
        /// <summary>
        /// 获取DateTime类型字段值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="fieldName">字段名</param>
        /// <returns>字段值</returns>
        public static DateTime FieldDateTime(this DataRow row, string fieldName) => row.Field<DateTime>(fieldName);
        /// <summary>
        /// 获取int类型字段值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="fieldName">字段名</param>
        /// <returns>字段值</returns>
        public static int FieldInt(this DataRow row, string fieldName) => row.Field<int>(fieldName);
        /// <summary>
        /// 获取short类型字段值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="fieldName">字段名</param>
        /// <returns>字段值</returns>
        public static short FieldShort(this DataRow row, string fieldName) => row.Field<short>(fieldName);
        /// <summary>
        /// 获取string类型字段值
        /// </summary>
        /// <param name="row">数据行</param>
        /// <param name="fieldName">字段名</param>
        /// <returns>字段值</returns>
        public static string FieldString(this DataRow row, string fieldName) => row.Field<string>(fieldName);
    }
}