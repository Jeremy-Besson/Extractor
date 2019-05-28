using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Castle.DynamicProxy;
using Extractor.AOP;
using Extractor.BigSets;
using Extractor.Constraints;

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

            List<IPrinter> printers = new List<IPrinter>();

            var bigSetFactory  = new BigSetFactoryWithInterceptor();
            //bigSetFactory = new BigSetFactory();
            //TOTO

            IPatternPrinter printer = new NullPatternPrinter();
            printer = new PatternPrinter();

            var data = Data.GenerateRandomData(bigSetFactory, 10000, 2, 0.3);

            
            var data2 = new Data(
                new List<IBigSet>()
                {
                    new BigSet(new List<int>() {1, 3, 4}),
                    new BigSet(new List<int>() {1, 3, 4}),
                    new BigSet(new List<int>() {1, 3, 4}),
                    new BigSet(new List<int>() {1, 3, 4}),
                }
            );

            //AM on the ASet and on the BSet should be applied at different places!!!!!
            List<IConstraint> AMconstraints = new List<IConstraint>()
            {
                //new MinSizeYesASet(0),
                new MinSizeASet(0),
                new ClosedBSet(),
            };

            List<IConstraint> Mconstraints = new List<IConstraint>()
            {
                //new MinSizeYesASet(2),
            };

            List<IConstraint> keepEnumeratingConstarints = new List<IConstraint>()
            {
                //new MaxSizeYesASet(2),
            };

            Extractor extractor = new Extractor(data, bigSetFactory , printer, AMconstraints, Mconstraints, keepEnumeratingConstarints);

            Stopwatch stopwatch = Stopwatch.StartNew();
            extractor.Extract();
            stopwatch.Stop();
            bigSetFactory.Print();

            Console.WriteLine("");
            Console.WriteLine($"Enumerated patterns:{extractor.GetNumberEnums()}");
            Console.WriteLine($"Extracted patterns:{extractor.GetNumberExtractedPatterns()}");

            //Console.WriteLine(printer.Print());

            Console.WriteLine("");
            Console.WriteLine($"Finished: {stopwatch.Elapsed}");

            return;

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
