using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Castle.DynamicProxy;
using Extractor.BigSets;

namespace Extractor.AOP
{
    class BigSetFactoryWithInterceptor: IBigSetFactory
    {
        ProxyGenerator generator = new ProxyGenerator();
        CallLoggingInterceptor callLogging = new CallLoggingInterceptor();
        Stopwatch Stopwatch1 = new Stopwatch();
        Stopwatch Stopwatch2 = new Stopwatch();

        public IBigSet Create(List<int> indexes)
        {
            IBigSet bigSet = generator.CreateClassProxy<BigSet>(callLogging);
            Stopwatch1.Start();
            bigSet.SetValues(indexes);
            Stopwatch1.Stop();
            return bigSet;
        }

        public IBigSet Clone(IBigSet bigSet)
        {
            //generator.CreateClassProxyWithTarget(typeof(BigSet), callLogging);
            BigSet bigSetProxy = generator.CreateClassProxy<BigSet>(callLogging);
            var options = new ProxyGenerationOptions { Selector = new DelegateSelector() };

            Stopwatch2.Start();
            ((BigSet)bigSet).data.ForEach(
                d =>
                {
                    bigSetProxy.data.Add(d);
                }
            );
            Stopwatch2.Stop();
            return bigSet;
        }

        public void Print()
        {
            callLogging.Print();
            Console.WriteLine($"Create: {Stopwatch1.Elapsed}");
            Console.WriteLine($"Clone: {Stopwatch2.Elapsed}");
        }
    }
}
