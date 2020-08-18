using EasyLogger.DbStorage;
using EasyLogger.DbStorage.Interface;
using EasyLogger.SqlSugarDbStorage.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EasyLogger.SqlSugarDbStorage.Impl
{
    public class SqlSugarRepository<TEntity, TPrimaryKey> : ISqlSugarRepository<TEntity, TPrimaryKey>
          where TEntity : class, IDbEntity<TPrimaryKey> , new()
    {
 
        public string ProviderName { get; private set; }
        public string OldProviderName { get; private set; }
        protected readonly ISqlSugarProviderStorage _sqlSugarProviderStorage;

        public SqlSugarRepository(ISqlSugarProviderStorage sqlSugarProviderStorage)
        {
            _sqlSugarProviderStorage = sqlSugarProviderStorage;
        }

        

        public IDisposable ChangeProvider(string name)
        {
            OldProviderName = ProviderName;
            ProviderName = name;
            return new DisposeAction(() =>
            {
                ProviderName = OldProviderName;
                OldProviderName = null;
            });
        }


        public SqlSugarClient GetCurrentSqlSugar()
        {
            return this._sqlSugarProviderStorage.GetByName(this.ProviderName, SqlSugarDbStorageConsts.DefaultProviderName).Sugar;
        }
      
        public int Insert(TEntity entity)
        {
            return this.GetCurrentSqlSugar().Insertable<TEntity>(entity).ExecuteCommand();
        }

        public List<TEntity> GetQuery(Expression<Func<TEntity, bool>> expression = null)
        {
            return this.GetCurrentSqlSugar().Queryable<TEntity>().Where(expression).ToList();
        }

        public void Dispose()
        {

        }


    }
}
