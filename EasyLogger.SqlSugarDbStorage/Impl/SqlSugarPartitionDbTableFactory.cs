using EasyLogger.DbStorage.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using EasyLogger.SqlSugarDbStorage;
using System.Text;
using EasyLogger.Model;

namespace EasyLogger.SqlSugarDbStorage.Impl
{
    public class SqlSugarPartitionDbTableFactory : IPartitionDbTableFactory
    {
       
        public void DbTableCreate(string path, bool isBaseDb)
        {
          
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = $@"Data Source={path}",
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true, // 自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute// 从实体特性中读取主键自增列信息
            });

            // 生成数据库
            // db.Ado.ExecuteCommand($"create dataabse {dbName}");

            if (isBaseDb)
            {
                db.CodeFirst.BackupTable().InitTables<EasyLoggerProject>();
            }
            else
            {

                CreateLoggerTable(db);
            }

            db.Dispose();
        }
        private static void CreateLoggerTable(SqlSugarClient db)
        {

            int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            for (int i = 1; i <= days; i++)
            {
                // 自定义生成表的别名
                db.MappingTables.Add(nameof(EasyLoggerRecord), $"{nameof(EasyLoggerRecord)}_{i}");
                db.CodeFirst.InitTables(typeof(EasyLoggerRecord));
            }

        }
    }
}
