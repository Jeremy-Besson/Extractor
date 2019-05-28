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
        private IBigSet A;
        private IBigSet B;
        private Stack<int> YesB = new Stack<int>();
        private List<int> ShouldBePresentB = new List<int>();
        private IBigSetFactory _bigSetFactory;
        private Data _data;

        private SearchSpace(IBigSetFactory bigSetFactory, Data data, IBigSet v1 = null, IBigSet v2 = null, List<int> yesB = null)
        {
            _bigSetFactory = bigSetFactory;
            _data = data;
        }

        public static ISearchSpace Create(IBigSetFactory bigSetFactory, Data data)
        {
            return new SearchSpace(bigSetFactory,data);
        }

        public virtual void SetSearchSpace(IBigSet v1, IBigSet v2, Stack<int> yesB = null)
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
                B = _bigSetFactory.Create(elemsB);
            }
            else
            {
                B = v2;
            }

            YesB = yesB ?? new Stack<int>();
        }

        public virtual int GetNextToEnumerateB()
        {
            return B.GetNextTrue();
        }

        public virtual IEnumerable<ISearchSpace> SplitB(int indexB)
        {
            B.SetFalse(indexB);

            //Not Contain
            ShouldBePresentB.Add(indexB);
            yield return this;

            //Contain
            ShouldBePresentB.Remove(indexB); ;
            var col = _data.getB(indexB);
            var A2 = _bigSetFactory.Clone(A);
            A2.Intersect(col);

            YesB.Push(indexB);
            
            var tmp = A;
            A = A2;
            yield return this;
            A = tmp;
            YesB.Pop();
            B.SetTrue(indexB);
        }


        public List<string> Print()
        {
            var yesS = "";
            if (YesB.Count > 0)
            {
                yesS = YesB.Select(x => x + "").ToList().Aggregate((a, b) => a + ", " + b);
            }

            var shouldBe = "";
            if (ShouldBePresentB.Count > 0)
            {
                shouldBe = ShouldBePresentB.Select(x => x + "").ToList().Aggregate((a, b) => a + ", " + b);
            }

            return new List<string>()
            {
                A.ToString(),
                B.ToString(),
                "YesB: {" + yesS + "}",
                "ShouldBeB: {" + shouldBe + "}",
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
            B = _bigSetFactory.Create(elemsB);
        }

        public Stack<int> GetYesB()
        {
            return YesB;
        }

        public IBigSet GetASet()
        {
            return A;
        }

        public IBigSet GetBSet()
        {
            return B;
        }

        public List<int> GetShouldBePresentB()
        {
            return ShouldBePresentB;
        }
    }
}
