using System;
using System.Collections.Generic;
using System.Text;

namespace Extractor
{
    class PatternPrinter : IPatternPrinter
    {
        public void PrintPattern(ISearchSpace searchSpace)
        {
            Console.WriteLine($"PATTERN: ");
            searchSpace.Print().ForEach(
                toPrint =>
                {
                    Console.WriteLine(toPrint);
                });
        }
    }
}
