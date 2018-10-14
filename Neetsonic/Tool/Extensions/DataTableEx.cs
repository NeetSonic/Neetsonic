using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Neetsonic.Tool.Extensions
{
    /// <summary>
    /// DataTable扩展类，用于转换成类实体对象
    /// </summary>
    public static class DataTableEx
    {
        /// <summary>
        /// 转换成对应的类对象集合
        /// </summary>
        /// <param name="dataTable">数据库查询结果表</param>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <returns>目标对象集合</returns>
        /// <exception cref="Exception">异常</exception>
        public static List<T> ToModel<T>(this DataTable dataTable) where T : class, new()
        {
            List<T> models = new List<T>();
            Type type = typeof(T);
            PropertyInfo[] propertys = type.GetProperties();// 获得此模型的公共属性
            try
            {
                foreach(DataRow row in dataTable.Rows)
                {
                    T model = new T();
                    foreach(PropertyInfo info in propertys)
                    {
                        if(info.CanWrite && dataTable.Columns.Contains(info.Name))
                        {
                            object value = row[info.Name];
                            if(value != DBNull.Value)
                            {
                                switch(info.PropertyType.FullName)
                                {
                                    case @"System.Decimal":
                                    {
                                        info.SetValue(model, Convert.ToDecimal(value));
                                        break;
                                    }
                                    case @"System.String":
                                    {
                                        info.SetValue(model, value.ToString());
                                        break;
                                    }
                                    case @"System.Int32":
                                    {
                                        info.SetValue(model, Convert.ToInt32(value));
                                        break;
                                    }
                                    default:
                                    {
                                        info.SetValue(model, value);
                                        break;
                                    }
                                }
                            }
                            else //空值
                            {
                                info.SetValue(model, null);
                            }
                        }
                    }
                    models.Add(model);
                }
            }
            catch
            {
                throw new Exception(@"属性包含不支持的数据类型!");
            }
            return models;
        }
    }
}