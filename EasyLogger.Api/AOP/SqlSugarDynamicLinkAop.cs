using Castle.DynamicProxy;
using EasyLogger.Api.Dtos;
using EasyLogger.Api.EasyTools;
using EasyLogger.SqlSugarDbStorage.Impl;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyLogger.SqlSugarDbStorage;
using Microsoft.Extensions.DependencyInjection;
using EasyLogger.SqlSugarDbStorage.Interface;
using SqlSugarProvider1 = EasyLogger.SqlSugarDbStorage.Impl.SqlSugarProvider;

namespace EasyLogger.Api.AOP
{
    public class SqlSugarDynamicLinkAop : DynamicLinkAopBase
    {
        private readonly IServiceProvider _serviceProvider;


        public override void Intercept(IInvocation invocation)
        {
            MethodInfo method;
            try
            {
                method = invocation.MethodInvocationTarget;
            }
            catch (Exception ex)
            {

                method = invocation.GetConcreteMethod();
            }


            var dynamicLinkAttr = GetDynamicLinkAttributeOrNull(method);
            if (dynamicLinkAttr == null || dynamicLinkAttr.IsDisabled)
            {
                invocation.Proceed();//直接执行被拦截方法
            }
            else
            {

                var input = this.GetTiemRange(invocation);

                var dateList = TimeTools.GetMonthByList(input.TimeStart.ToString("yyyy-MM"), input.TimeEnd.ToString("yyyy-MM"));

                foreach (var item in dateList)
                {
                    var DbName = $"{IocManager.Configuration["EasyLogger:DbName"]}-{item.ToString("yyyy-MM")}";
                    var dbPathName = Path.Combine(PathExtenstions.GetApplicationCurrentPath(), DbName + ".db");

                    IocManager.ServiceProvider.AddSqlSugarDatabaseProvider(new SqlSugarSetting()
                    {
                        Name = DbName,
                        ConnectionString = @$"Data Source={dbPathName}",
                        DatabaseType = DbType.Sqlite,
                        LogExecuting = (sql, pars) =>
                        {
                            Console.WriteLine($"sql:{sql}");
                        }
                    });

                }


                invocation.Proceed();//直接执行被拦截方法
            }


        }
    }
}
