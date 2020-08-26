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

        public static IServiceProvider Build(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            Services = serviceDescriptors;
            ServiceProvider = Services.BuildServiceProvider();
            Configuration = configuration;
            return ServiceProvider;
        }
    }
}
