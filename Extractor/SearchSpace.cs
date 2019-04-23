using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Extractor.AOP;
using Extractor.BigSets;

namespace Extractor
{
    public class SearchSpace : ISearchSpace
    {
        public IBigSet A;
        public IBigSet B;
        public List<int> YesB = new List<int>();
        private IBigSetFactory _bigSetFactory;
        private Data _data;
        static ProxyGenerator generator = new ProxyGenerator();
        static CallLoggingInterceptor callLogging = new CallLoggingInterceptor();

        
        private SearchSpace(IBigSetFactory bigSetFactory, Data data, IBigSet v1 = null, IBigSet v2 = null, List<int> yesB = null)
        {
            _bigSetFactory = bigSetFactory;
            _data = data;
        }
        

        public static ISearchSpace Create(IBigSetFactory bigSetFactory, Data data)
        {
            return new SearchSpace(bigSetFactory,data);

//            SearchSpace bigSet = (SearchSpace) generator.CreateClassProxy<SearchSpace>(callLogging);
//            bigSet._bigSetFactory = bigSetFactory;
//            bigSet._data = data;
//            return bigSet;
        }

        public virtual void Copy(IBigSet v1, IBigSet v2, List<int> yesB = null)
        {
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


        public virtual int GetNextToEnumerateB()
        {
            return B.GetNextTrue();
        }


        public virtual IEnumerable<ISearchSpace> SplitB(int indexB)
        {
            B.SetFalse(indexB);

            var yes1 = new List<int>(YesB);
            var sp1 = SearchSpace.Create(_bigSetFactory, _data);
            sp1.Copy(A, B, yes1);
            yield return this;

            var yes2 = new List<int>(YesB) {indexB};
            var col = _data.getB(indexB);
            var A2 = _bigSetFactory.Clone(A);
            A2.Intersect(col);
            var sp2 = SearchSpace.Create(_bigSetFactory, _data);
            sp2.Copy(A2,B,yes2);
            yield return sp2;

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

        public virtual void Init()
        {
                List<int> elemsA = new List<int>();
                Enumerable.Range(1, _data.NumberA()).ToList().ForEach(
                    x => { elemsA.Add(x); }
                );
                A = _bigSetFactory.Create(elemsA);

                List<int> elemsB = new List<int>();
                Enumerable.Range(1, _data.NumberB()).ToList().ForEach(
                    x => { elemsB.Add(x); }
                );
                B = new BigSet(elemsB);
        }
    }
}
