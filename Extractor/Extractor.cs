using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extractor.BigSets;
using Extractor.Constraints;

namespace Extractor
{
    class Extractor
    {
        private Data _data;
        private readonly IBigSetFactory _bigSetFactory;
        private IPatternPrinter _patternPrinter;
        private readonly List<IConstraint> _AMconstraints;
        private List<IConstraint> KeepEnumeratingConstarints { get; }
        private List<IConstraint> _Mconstraints { get; }
        private int extractedPattern = 0;
        private int enumNumber = 0;



        public Extractor(Data data, IBigSetFactory bigSetFactory, IPatternPrinter patternPrinter, List<IConstraint> AMconstraints, List<IConstraint> Mconstraints, List<IConstraint> keepEnumeratingConstarints)
        {
            _data = data;
            _bigSetFactory = bigSetFactory;
            _patternPrinter = patternPrinter;
            KeepEnumeratingConstarints = keepEnumeratingConstarints;
            _AMconstraints = AMconstraints;
            _Mconstraints = Mconstraints;
        }

        public int GetNumberExtractedPatterns()
        {
            return extractedPattern;
        }

        public int GetNumberEnums()
        {
            return enumNumber;
        }

        public void Extract()
        {
            ISearchSpace searchSpace = SearchSpace.Create(_bigSetFactory, _data);
            searchSpace.Init();
            Enum(searchSpace);
        }

        private void Enum(ISearchSpace searchSpace)
        {
            var satisfyAMConstarints = _AMconstraints?.All(c =>
            {
                return c.Satisfy(searchSpace, _data);
            }) ?? true;

            if (satisfyAMConstarints)
            {
                enumNumber++;
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
                    var satisfyMConstraint = _Mconstraints?.All(c =>
                    {
                        return c.Satisfy(searchSpace, _data);
                    }) ?? true;
                    if (satisfyMConstraint)
                    {
                        extractedPattern++;
                        _patternPrinter.PrintPattern(searchSpace);
                    }
                }
            }
            else
            {
                /*
                Console.WriteLine("Failed");
                searchSpace.Print().ForEach(
                    toPrint =>
                    {
                        Console.WriteLine(toPrint);
                    }
                    );
                    */

            }
        }
    }
}
