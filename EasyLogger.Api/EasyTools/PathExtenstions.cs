using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.EasyTools
{
    public class PathExtenstions
    {
        public static string GetApplicationCurrentPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "../";
        }
    }
}
