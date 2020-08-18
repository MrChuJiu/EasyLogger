using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.Dtos.EasyLoggerProjectDto
{
    public class EasyLoggerProjectListDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 系统编码
        /// </summary>
        public string Code { get; set; }
    }
}
