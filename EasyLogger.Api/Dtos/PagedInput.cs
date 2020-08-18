using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.Dtos
{
    public class PagedInput
    {
        public Int32 PageSize { get; set; }
        public Int32 PageIndex { get; set; }
    }
}
