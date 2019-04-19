using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Extractor
{
    class Program
    {
        

        public void Enum(SearchSpace searchSpace)
        {
            int toEnumerateB = searchSpace.GetNextToEnumerateB();

            if(toEnumerateB != -1)
            {
                using (IEnumerator<SearchSpace> enumerator = searchSpace.SplitB(toEnumerateB))
                    while (enumerator.MoveNext())
                    {
                        
                        var currentSearchSpace = enumerator.Current;

                        //Console.WriteLine($"A: {PrintHelper.Print(currentSearchSpace.A)}");
                        //Console.WriteLine($"B: {PrintHelper.Print(currentSearchSpace.B)}");

                        Enum(currentSearchSpace);
                    }
            }
            else
            {
                Console.WriteLine($"PATTERN: ");
                Console.WriteLine($"A: {PrintHelper.Print(searchSpace.A)}");
                Console.WriteLine($"B: {PrintHelper.Print(searchSpace.B)}");

            }
        }

        static void Main(string[] args)
        {


            Data data = new Data();


            SearchSpace searchSpace = new SearchSpace(15, 11, data );
            Program program = new Program();
            program.Enum(searchSpace);



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
