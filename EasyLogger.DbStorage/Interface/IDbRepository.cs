using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EasyLogger.DbStorage.Interface
{
    public interface IDbRepository<TEntity, TPrimaryKey> : IDisposable
    {
        /// <summary>
        /// 修改Provider
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDisposable ChangeProvider(string name);


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(TEntity entity);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetQuery(Expression<Func<TEntity, bool>> expression = null);

    }
}
