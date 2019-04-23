using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extractor.BigSets;

namespace Extractor
{
    public class SearchSpace
    {
        public IBigSet A;
        public IBigSet B;
        public List<int> YesB = new List<int>();
        private readonly IBigSetFactory _bigSetFactory;
        private readonly Data _data;

        public SearchSpace(IBigSetFactory bigSetFactory, Data data, IBigSet v1 = null, IBigSet v2 = null, List<int> yesB = null)
        {
            _bigSetFactory = bigSetFactory;
            _data = data;

            if (v1 == null)
            {
                List<int> elemsA = new List<int>();
                Enumerable.Range(1, _data.NumberA()).ToList().ForEach(
                    x => { elemsA.Add(x); }
                );
                A = _bigSetFactory.Create(elemsA);
            }
            else
            {
                A = v1;
            }

            if (v2 == null)
            {
                List<int> elemsB = new List<int>();
                Enumerable.Range(1, _data.NumberB()).ToList().ForEach(
                    x => { elemsB.Add(x); }
                );
                B = new BigSet(elemsB);
            }
            else
            {
                B = v2;
            }

            YesB = yesB ?? new List<int>();
        }

        internal int GetNextToEnumerateB()
        {
            return B.GetNextTrue();
        }

        public IEnumerable<SearchSpace> SplitB(int indexB)
        {
            B.SetFalse(indexB);
            var yes1 = new List<int>(YesB);
            yield return new SearchSpace(_bigSetFactory, _data, A, B, yes1);

            var yes2 = new List<int>(YesB) {indexB};
            var col = _data.getB(indexB);
            var A2 = _bigSetFactory.Clone(A);
            A2.Intersect(col);
            //B.Clone not necessary!!! _bigSetFactory.Clone(B)
            yield return new SearchSpace(_bigSetFactory, _data, A2, B, yes2);
            B.SetTrue(indexB);
        }

        public List<string> Print()
        {
            var yesS = "";
            if (YesB.Count > 0)
            {
                yesS = YesB.Select(x => x + "").ToList().Aggregate((a, b) => a + ", " + b);
            }

            return new List<string>()
            {
                A.ToString(),
                B.ToString(),
                "{" + yesS + "}",
            };
        }
    }
}
