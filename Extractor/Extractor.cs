using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extractor.BigSets;

namespace Extractor
{
    class Extractor
    {
        private Data _data;
        private readonly IBigSetFactory _bigSetFactory;
        private IPatternPrinter _patternPrinter;

        public Extractor(Data data, IBigSetFactory bigSetFactory, IPatternPrinter patternPrinter)
        {
            _data = data;
            _bigSetFactory = bigSetFactory;
            _patternPrinter = patternPrinter;
        }

        public void Extract()
        {
            SearchSpace searchSpace = new SearchSpace(_bigSetFactory, _data);

            Enum(searchSpace);
        }

        private void Enum(SearchSpace searchSpace)
        {
            int toEnumerateB = searchSpace.GetNextToEnumerateB();

            if (toEnumerateB != -1)
            {
                var split = searchSpace.SplitB(toEnumerateB);
                using (var en = split.GetEnumerator())
                {
                    while (en.MoveNext())
                    {
                        Enum(en.Current);
                    }
                }
            }
            else
            {
                _patternPrinter.PrintPattern(searchSpace);
            }
        }
    }
}
