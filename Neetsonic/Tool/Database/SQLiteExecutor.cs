using System.Data;
using System.Data.SQLite;

namespace Neetsonic.Tool.Database
{
    /// <summary>
    /// SQLite数据库执行器
    /// </summary>
    public static class SQLiteExecutor
    {
        /// <summary>
        /// 测试数据库连接是否成功
        /// </summary>
        /// <param name="connection">数据库连接字符串</param>
        /// <returns>连接是否成功</returns>
        public static bool ConnectSucceed(string connection)
        {
            try
            {
                using(SQLiteConnection conn = new SQLiteConnection(connection))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 执行不带返回值的Sql语句
        /// </summary>
        /// <param name="connection">数据库连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数集</param>
        public static void ExecuteNonQuery(string connection, string sql, SQLiteParameter[] parameters)
        {
            using(SQLiteConnection conn = new SQLiteConnection(connection))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                if(null != parameters)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 执行带有返回值的Sql语句
        /// </summary>
        /// <param name="connection">数据库连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数集</param>
        /// <returns>返回值</returns>
        public static DataSet ExecuteQuery(string connection, string sql, SQLiteParameter[] parameters)
        {
            using(SQLiteConnection conn = new SQLiteConnection(connection))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                if(null != parameters)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                using(SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    return dataSet;
                }
            }
        }
        /// <summary>
        /// 执行单一返回值的Sql语句
        /// </summary>
        /// <param name="connection">数据库连接字符串</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">参数集</param>
        /// <returns>单一返回值</returns>
        public static object ExecuteScalar(string connection, string sql, SQLiteParameter[] parameters)
        {
            using(SQLiteConnection conn = new SQLiteConnection(connection))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                if(null != parameters)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                return cmd.ExecuteScalar();
            }
        }
    }
}