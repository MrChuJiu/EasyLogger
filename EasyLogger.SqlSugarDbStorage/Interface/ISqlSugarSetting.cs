using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLogger.SqlSugarDbStorage.Interface
{
    public interface ISqlSugarSetting
    {

        /// <summary>
        /// 配置名称Kety
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string ConnectionString { get; set; }
        /// <summary>
        /// 数据库类型呢
        /// </summary>
        DbType DatabaseType { get; set; }
        /// <summary>
        /// 使用Sql执行日志
        /// </summary>
        Action<string, SugarParameter[]> LogExecuting { get; set; }

    }
}
