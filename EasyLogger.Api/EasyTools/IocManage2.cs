using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.EasyTools
{
    public class IocManage2
    {
        private IServiceProvider _provider;

        private IServiceCollection _services;

        /// <summary>
        /// Ioc管理实例
        /// </summary>
        private static readonly Lazy<IocManage2> InstanceLazy = new Lazy<IocManage2>(() => new IocManage2());
        private IocManage2()
        {

        }



        public static IocManage2 Instance => InstanceLazy.Value;


        /// <summary>
        /// 设置应用程序服务提供者
        /// </summary>
        internal void SetApplicationServiceProvider(IServiceProvider provider)
        {
          
            _provider = provider;

        }

        internal void SetServiceCollection(IServiceCollection services)
        {

         
            _services = services;
        }







        /// <summary>
        /// 得到服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>

        public T GetService<T>()
        {
           
            return _provider.GetService<T>();
        }



        /// <summary>
        /// 得到日志记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ILogger GetLogger<T>()
        {
            ILoggerFactory factory = _provider.GetService<ILoggerFactory>();
            return factory.CreateLogger<T>();
        }

    }
}
