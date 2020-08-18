
using EasyLogger.DbStorage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Model
{
    public class EasyLoggerRecord : IDbEntity<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// 类型.自定义标签
        /// </summary>
        public string LogType { get; set; }
        /// <summary>
        /// 状态-成功、失败、警告等
        /// </summary>
        //public LogState LogState { get; set; }
        public string LogState { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string LogTitle { get; set; }
        /// <summary>
        /// 内容描述
        /// </summary>
        public string LogContent { get; set; }
        /// <summary>
        /// 在系统中产生的时间
        /// </summary>
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
