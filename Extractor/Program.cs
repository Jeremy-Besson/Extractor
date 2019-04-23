using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Castle.DynamicProxy;
using Extractor.AOP;
using Extractor.BigSets;

namespace Extractor
{
    class Program
    {
        /*
        public void InitializeProxy()
        {
            IBar bar = new Bar();
            var pg = new ProxyGenerator();
            this.BarInterfaceProxy = pg.CreateInterfaceProxyWithTarget(bar, new CacheInterceptor());
            this.FooBarProxy = pg.CreateClassProxy<FooBar>(new CacheInterceptor());
        }
        */
        static void Main(string[] args)
        {

            //var proxy = generator.CreateClassProxyWithTarget<BigSet>(typeof(IBigSet), new CallLoggingInterceptor());
            //container.AddFacility<Castle.Facilities.FactorySupport.FactorySupportFacility>();
            //var proxy =  generator.CreateInterfaceProxyWithTargetInterface( typeof(IBigSet), new CallLoggingInterceptor());
            //var comparer = DelegateWrapper.WrapAs<IBigSet>(new CallLoggingInterceptor());

            
            var data = Data.GenerateRandomData(22,1000,0.1);

            Stopwatch stopwatch = Stopwatch.StartNew();
            var bigSetFactoryWithInterceptor  = new BigSetFactoryWithInterceptor();
            //var bigSetFactoryWithInterceptor = new BigSetFactory();
            Extractor extractor = new Extractor(data, bigSetFactoryWithInterceptor ,  new NullPatternPrinter());
            extractor.Extract();
            stopwatch.Stop();
            bigSetFactoryWithInterceptor.Print();

            

            Console.WriteLine($"Finished: {stopwatch.Elapsed}");

            Console.WriteLine("Hello World!");
            int firstNumber = 14, secondNumber = 11, result;
            result = firstNumber & secondNumber;
            Console.WriteLine("{0} & {1} = {2}", firstNumber, secondNumber, result);

            Console.WriteLine(Convert.ToString(firstNumber, 2).PadLeft(32, '0'));

            Random Rand = new Random();
            //converting byte --> bool[] as bits of a byte
            byte val = (byte)Rand.Next(256);
            BitArray t = new BitArray(new byte[] { val });
            Console.WriteLine("The value is {0}", val);
            bool[] bits = new bool[8];
            t.CopyTo(bits, 0);
            if (!BitConverter.IsLittleEndian) Array.Reverse(bits); //IMPORTANT!

            //printing out the bit array as a binary number
            Console.Write("Binary: ");
            for (int x = 7; x >= 0; x--) Console.Write(bits[x] ? "1" : "0");
            Console.WriteLine();

            //various binary operations can be done to the bool[]
            if (bits[7]) Console.WriteLine("Top bit is set");

            //converting bool[] as bits --> byte
            byte[] temp = new byte[1];
            t.CopyTo(temp, 0);
            Console.WriteLine("The value is still {0}", temp[0]);


            Console.Read();
        }
    }
}
