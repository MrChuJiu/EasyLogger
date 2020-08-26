using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.Dtos.EasyLoggerRecordDto
{
    public class EasyLoggerRecordInput : DynamicLinkInput
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int? ProjectId { get; set; }
        /// <summary>
        /// 类型.自定义标签
        /// </summary>
        public string LogType { get; set; }
        /// <summary>
        /// 状态-成功、失败、警告等
        /// </summary>
        public string LogState { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string LogTitle { get; set; }
    }
}
