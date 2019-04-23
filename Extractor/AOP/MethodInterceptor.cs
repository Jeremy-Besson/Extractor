using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace Extractor.AOP
{
    internal class MethodInterceptor : IInterceptor
    {
        public readonly Delegate _impl;

        public MethodInterceptor(Delegate @delegate)
        {
            this._impl = @delegate;
        }

        public void Intercept(IInvocation invocation)
        {
            var result = this._impl.DynamicInvoke(invocation.Arguments);
            invocation.ReturnValue = result;
        }
    }
}
