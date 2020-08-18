using EasyLogger.DbStorage.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.Model
{
    public class EasyLoggerProject: IDbEntity<int>
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 系统编码
        /// </summary>
        public string Code { get; set; }

    }
}
