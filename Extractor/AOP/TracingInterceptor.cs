using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Castle.DynamicProxy;

namespace Extractor.AOP
{
    public class TracingInterceptor : IInterceptor
    {
        private int _indentation = 0;
        Dictionary<string, Stopwatch> _stopwatches = new Dictionary<string, Stopwatch>();
        Dictionary<string, int> _inc = new Dictionary<string, int>();
        public void Intercept(IInvocation invocation)
        {
            _indentation++;
            var sw = GetStopWatch(invocation.Method.Name);
            _inc[invocation.Method.Name] = GetInc(invocation.Method.Name) + 1;
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

        private Stopwatch GetStopWatch(string methodName)
        {
            if (!_stopwatches.ContainsKey(methodName))
            {
                _stopwatches.Add(methodName, new Stopwatch());
            }

            return _stopwatches[methodName];
        }
        private int GetInc(string methodName)
        {
            if (!_inc.ContainsKey(methodName))
            {
                _inc.Add(methodName, 0);
            }

            return _inc[methodName];
        }
        public void Print()
        {
            _stopwatches.ToList().ForEach(
                sw =>
                {
                    Console.WriteLine($"{sw.Key} (time) ! {sw.Value.Elapsed}");
                    Console.WriteLine($"{sw.Key} (number)! {_inc[sw.Key]}");
                }
                );
        }
    }
}
