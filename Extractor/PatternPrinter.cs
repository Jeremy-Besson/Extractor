using System;
using System.Collections.Generic;
using System.Text;

namespace Extractor
{
    class PatternPrinter : IPatternPrinter
    {
        public void PrintPattern(SearchSpace searchSpace)
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
