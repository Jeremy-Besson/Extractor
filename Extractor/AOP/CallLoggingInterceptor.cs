using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;

namespace Extractor.AOP
{
    public class CallLoggingInterceptor : IInterceptor
    {
        private int _indentation = 0;
        Dictionary<string,Stopwatch> _stopwatches = new Dictionary<string, Stopwatch>() ;
        public void Intercept(IInvocation invocation)
        {
            try
            {
                _indentation++;
                var sw = GetStopWatch(invocation.Method.Name);
                sw.Start();
                try
                {
                    invocation.Proceed();
                }
                catch (Exception ex)
                {
                    var tt = "";
                }

                sw.Stop();
            }
            finally
            {
                //_indentation--;
            }
        }

        private Stopwatch GetStopWatch(string methodName)
        {
            if (!_stopwatches.ContainsKey(methodName))
            {
                _stopwatches.Add(methodName,new Stopwatch());
            }

            return _stopwatches[methodName];
        }

        public void Print()
        {
            _stopwatches.ToList().ForEach(
                sw =>
                {
                    Console.WriteLine($"{sw.Key} ! {sw.Value.Elapsed}");
                }
                );
        }
    }
}
