using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.AOP
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class DynamicLinkAttribute: Attribute
    {
        public bool IsDisabled { get; set; }
    }
}
