using System;
using System.Collections.Generic;
using System.Text;

namespace Extractor
{
    class NullPatternPrinter : IPatternPrinter, IPrinter
    {
        int i = 0;

        public string Print()
        {
            return $"Extracted patterns: {i}";
        }

        public void PrintPattern(ISearchSpace searchSpace)
        {
            i++;

            //Console.WriteLine(i);
        }
    }
}
