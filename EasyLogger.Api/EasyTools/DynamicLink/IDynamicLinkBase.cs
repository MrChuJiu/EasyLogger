using EasyLogger.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.EasyTools.DynamicLink
{
    public interface IDynamicLinkBase
    {
        abstract List<DateTime> DynamicLinkOrm(DynamicLinkInput input);
    }
}
