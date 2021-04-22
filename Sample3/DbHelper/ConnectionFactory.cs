using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using Sample03.Extensions;
using Sample03.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sample03.DbHelper
{
    /// <summary>
    /// 数据库连接工厂类
    /// </summary>
    public class ConnectionFactory
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static IDbConnection CreateConnection(string dbType, string connStr)
        {
            if (dbType.IsNullOrWhiteSpace())
                throw new ArgumentNullException("数据库类型为空");
            if (connStr.IsNullOrWhiteSpace())
                throw new ArgumentNullException("数据库连接字符串为空");

            var type = GetDataBaseType(dbType);
            return CreateConnection(type, connStr);
        }


        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public static IDbConnection CreateConnection(DatabaseType dbType, string connStr)
        {
            IDbConnection connection = null;

            switch (dbType)
            {
                case DatabaseType.SqlServer:
                    connection = new SqlConnection(connStr);
                    break;
                case DatabaseType.MySQL:
                    connection = new MySqlConnection(connStr);
                    break;
                default:
                    throw new ArgumentNullException($"不支持的数据库类型：{dbType.ToString()}");
            }

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection;
        }

        /// <summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        public static DatabaseType GetDataBaseType(string dbtype)
        {
            DatabaseType databaseType = DatabaseType.SqlServer;
            foreach (DatabaseType type in Enum.GetValues(typeof(DatabaseType)))
            {
                if (type.ToString().Equals(dbtype, StringComparison.OrdinalIgnoreCase))
                {
                    databaseType = type;
                    break;
                }
            }
            return databaseType;
        }
        
    }
}
