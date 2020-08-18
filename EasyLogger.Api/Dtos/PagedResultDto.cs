using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.Dtos
{
    public class PagedResultDto<T>
    {
        public List<T> List { get; set; }
        public long Total { get; set; }
    }
}
