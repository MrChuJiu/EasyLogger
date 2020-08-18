using EasyLogger.SqlSugarDbStorage.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLogger.SqlSugarDbStorage.Impl
{
    public class SqlSugarSetting : ISqlSugarSetting
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public DbType DatabaseType { get; set; }
        public Action<string, SugarParameter[]> LogExecuting { get; set; }
    }
}
