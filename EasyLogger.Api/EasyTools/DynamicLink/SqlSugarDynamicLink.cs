using EasyLogger.Api.Dtos;
using EasyLogger.SqlSugarDbStorage.Impl;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyLogger.SqlSugarDbStorage;
using Autofac.Extras.DynamicProxy;
using EasyLogger.Api.AOP;

namespace EasyLogger.Api.EasyTools.DynamicLink
{
    [Intercept(typeof(SqlSugarDynamicLinkAop))]
    public class SqlSugarDynamicLink : IDynamicLinkBase
    {
        [DynamicLink]
        public virtual List<DateTime> DynamicLinkOrm(DynamicLinkInput input)
        {
            return TimeTools.GetMonthByList(input.TimeStart.ToString("yyyy-MM"), input.TimeEnd.ToString("yyyy-MM"));
        }
    }
}
