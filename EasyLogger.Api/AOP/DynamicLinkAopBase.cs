using Castle.DynamicProxy;
using EasyLogger.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace EasyLogger.Api.AOP
{
    public abstract class DynamicLinkAopBase : IInterceptor
    {
        /// <summary>
        ///  AOP的拦截方法
        /// </summary>
        /// <param name="invocation"></param>
        public abstract void Intercept(IInvocation invocation);

        /// <summary>
        /// 获取查询所需的必要条件
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public DynamicLinkInput GetTiemRange(IInvocation invocation) {
            var methodArguments = invocation.Arguments.FirstOrDefault();//获取参数列表
            var input = (DynamicLinkInput)methodArguments;
            return input;
        }

        public DynamicLinkAttribute GetDynamicLinkAttributeOrNull(MethodInfo methodInfo) {

            var attrs = methodInfo.GetCustomAttributes(true).OfType<DynamicLinkAttribute>().ToArray();
            if(attrs.Length > 0) {
                return attrs[0];
            }
            attrs = methodInfo.DeclaringType.GetTypeInfo().GetCustomAttributes(true).OfType<DynamicLinkAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return attrs[0];
            }
            return null;
        }



    }
}
