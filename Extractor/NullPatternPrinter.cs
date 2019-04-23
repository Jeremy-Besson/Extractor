using System;
using System.Collections.Generic;
using System.Text;

namespace Extractor
{
    class NullPatternPrinter : IPatternPrinter
    {
        int i = 0;
        public void PrintPattern(ISearchSpace searchSpace)
        {
            i++;

            //Console.WriteLine(i);
        }
    }
}
