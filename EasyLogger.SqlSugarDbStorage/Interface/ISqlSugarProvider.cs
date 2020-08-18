using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLogger.SqlSugarDbStorage.Interface
{
    public interface ISqlSugarProvider: IDisposable
    {
        /// <summary>
        /// 针对这个连接起别名
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// SqlSugar实例
        /// </summary>
        SqlSugarClient Sugar { get; }
    }
}
