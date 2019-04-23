using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Extractor.AOP;

namespace Extractor
{
    public class DelegateWrapper
    {
        public static T WrapAs<T>(Delegate impl) where T : class
        {
            var generator = new ProxyGenerator();
            var proxy = generator.CreateInterfaceProxyWithoutTarget(typeof(T), new MethodInterceptor(impl));
            return (T)proxy;
        }
    }
}
