using EasyLogger.DbStorage.Interface;
using EasyLogger.SqlSugarDbStorage.Impl;
using EasyLogger.SqlSugarDbStorage.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLogger.SqlSugarDbStorage
{
    public static class SqlSugarDbStorageServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlSugarDbStorage(this IServiceCollection services,
         ISqlSugarSetting defaultDbSetting)
        {
            if (defaultDbSetting == null)
            {
                throw new ArgumentNullException(nameof(defaultDbSetting));
            }

            services.AddSingleton<ISqlSugarProvider>(new SqlSugarProvider(defaultDbSetting));
            services.AddTransient(typeof(ISqlSugarRepository<,>), typeof(SqlSugarRepository<,>));
            services.AddTransient(typeof(IDbRepository<,>), typeof(SqlSugarRepository<,>));
            services.AddSingleton<ISqlSugarProviderStorage, DefaultSqlSugarProviderStorage>();
            services.AddSingleton<IPartitionDbTableFactory, SqlSugarPartitionDbTableFactory>();

            return services;

        }
    }
}
