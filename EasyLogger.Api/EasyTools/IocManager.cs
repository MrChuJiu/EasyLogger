using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.EasyTools
{
    public class IocManager
    {
        public static IServiceCollection Services { get; private set; }

        public static IServiceProvider ServiceProvider { get; private set; }

        public static IConfiguration Configuration { get; private set; }

        static IocManager()
        {
            Services = new ServiceCollection();
        }

        public static IServiceProvider Build()
        {
            ServiceProvider = Services.BuildServiceProvider();
            return ServiceProvider;
        }

        public static void SetConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            if (ServiceProvider == null) {
                return;
            }
            ServiceProvider = serviceProvider;
        }


    }
}
