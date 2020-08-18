using EasyLogger.DbStorage.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLogger.SqlSugarDbStorage.Interface
{
    public interface ISqlSugarRepository<TEntity, TPrimaryKey> : IDbRepository<TEntity, TPrimaryKey>
            where TEntity : class, IDbEntity<TPrimaryKey>
    {
        /// <summary>
        /// 获取sqlSugar对象
        /// </summary>
        /// <returns></returns>
        SqlSugarClient GetCurrentSqlSugar();
    }
}
