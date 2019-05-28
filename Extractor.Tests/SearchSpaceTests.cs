using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Extractor;
using Extractor.BigSets;
using NUnit.Framework;

namespace Extractor.Tests
{
    public class SearchcurTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        /*
        [Test]
        public void Scenario1()
        {
            Data data = new Data(
                new List<IBigSet>()
                {
                    new BigSet(new List<int>() {1}),
                    new BigSet(new List<int>() {2}),
                }
            );

            BigSet b1 = new BigSet(new List<int>() {1, 2});
            BigSet b2 = new BigSet(new List<int>() {1, 2});

            ISearchSpace searchcur = SearchSpace.Create(new BigSetFactory(), data);

            var split1 = searchcur.SplitB(1).ToList();
            var split2 = searchcur.SplitB(2).ToList();

        }
        */
        
        [Test]
        public void Create()
        {
            Data data = new Data(
                new List<IBigSet>()
                {
                    new BigSet(new List<int>() {1, 3, 4}),
                    new BigSet(new List<int>() {2, 4}),
                    new BigSet(new List<int>() {1, 3, 4}),
                    new BigSet(new List<int>() {1, 4}),
                }
            );

            BigSet b1 = new BigSet(new List<int>() {1, 2, 3, 4});
            BigSet b2 = new BigSet(new List<int>() {1, 2, 3, 4});

            ISearchSpace searchcur = SearchSpace.Create(new BigSetFactory(), data);
            searchcur.SetSearchSpace(b1, b2);

            var split = searchcur.SplitB(1);

            int i = 0;
            var en = split.GetEnumerator();
            while (en.MoveNext())
            {
                var cur = en.Current;
                if (i == 0)
                {
                    Assert.True(cur.GetASet().Equals(new BigSet(new List<int>() {1, 2, 3, 4})));
                    Assert.True(cur.GetBSet().Equals(new BigSet(new List<int>() {2, 3, 4})));
                    Assert.AreEqual(0, cur.GetYesB().Count);
                }
                else
                {
                    Assert.True(cur.GetASet().Equals(new BigSet(new List<int>() {1, 3, 4})));
                    Assert.True(cur.GetBSet().Equals(new BigSet(new List<int>() {2, 3, 4})));
                    Assert.AreEqual(1, cur.GetYesB().Count);
                }

                i++;
            }

            Assert.AreEqual(2, i);

            /*
            Assert.True(searchcur.A.Equals(new BigSet(new List<int>() { 1, 2, 3, 4 })));
            Assert.True(searchcur.B.Equals(new BigSet(new List<int>() { 1, 2, 3, 4 })));
    
            b1 = new BigSet(new List<int>() { 1, 2, 3, 4 });
            b2 = new BigSet(new List<int>() { 1, 2, 3, 4 });
            searchcur = new Searchcur(b1, b2, data);
            split = searchcur.SplitB(3).ToList();
            Assert.AreEqual(2, split.Count);
            Assert.True(split[0].A.Equals(new BigSet(new List<int>() { 1, 2, 3, 4 })));
            Assert.True(split[0].B.Equals(new BigSet(new List<int>() {1, 2, 4 })));
            Assert.True(split[1].A.Equals(new BigSet(new List<int>() { 1, 3, 4 })));
            Assert.True(split[1].B.Equals(new BigSet(new List<int>() { 1, 2, 4 })));
            Assert.True(searchcur.A.Equals(new BigSet(new List<int>() { 1, 2, 3, 4 })));
            Assert.True(searchcur.B.Equals(new BigSet(new List<int>() { 1, 2, 3, 4 })));
    
            b1 = new BigSet(new List<int>() { 1, 2, 3, 4 });
            b2 = new BigSet(new List<int>() { 1, 2, 3, 4 });
            searchcur = new Searchcur(b1, b2, data);
            split = searchcur.SplitB(4).ToList();
            Assert.AreEqual(2, split.Count);
            Assert.True(split[0].A.Equals(new BigSet(new List<int>() { 1, 2, 3, 4 })));
            Assert.True(split[0].B.Equals(new BigSet(new List<int>() { 1, 2, 3 })));
            Assert.True(split[1].A.Equals(new BigSet(new List<int>() { 1,4 })));
            Assert.True(split[1].B.Equals(new BigSet(new List<int>() { 1, 2, 3 })));
            Assert.True(searchcur.A.Equals(new BigSet(new List<int>() { 1, 2, 3, 4 })));
            Assert.True(searchcur.B.Equals(new BigSet(new List<int>() { 1, 2, 3, 4 })));
            */
        }

        [Test]
        public void BigSet_Create()
        {
            BigSet b = new BigSet(new List<int>());
            Assert.AreEqual(1, b.data.Count);
            Assert.AreEqual(0, b.data[0]);
            b.SetTrue(1);
            Assert.AreEqual(1, b.data[0]);
            b.SetTrue(1);
            Assert.AreEqual(1, b.data[0]);

            b = new BigSet(new List<int>());
            b.SetTrue(2);
            Assert.AreEqual(2, b.data[0]);

            var ind = 4;
            b = new BigSet(new List<int>());
            b.SetTrue(ind);
            Assert.AreEqual(Math.Pow(2, ind - 1), b.data[0]);

            ind = 10;
            b = new BigSet(new List<int>());
            b.SetTrue(ind);
            Assert.AreEqual(System.Numerics.BigInteger.Pow(2, ind - 1), new BigInteger(b.data[0]));

            ind = 13;
            b = new BigSet(new List<int>());
            b.SetTrue(ind);
            Assert.AreEqual(System.Numerics.BigInteger.Pow(2, ind - 1), new BigInteger(b.data[0]));

            ind = 32;
            b = new BigSet(new List<int>());
            b.SetTrue(ind);
            b.Print();
            Assert.AreEqual(System.Numerics.BigInteger.Pow(2, ind - 1), new BigInteger(b.data[0]));

            ind = 64;
            b = new BigSet(new List<int>());
            b.SetTrue(ind);
            b.Print();
            Assert.AreEqual(System.Numerics.BigInteger.Pow(2, ind - 1), new BigInteger(b.data[0]));


            //Math.Pow(2,63)
            b = new BigSet(new List<int>() {1});
            Assert.AreEqual(1, b.data.Count);
            Assert.AreEqual(1, b.data[0]);

            b = new BigSet(new List<int>() {65});
            Assert.AreEqual(2, b.data.Count);
            Assert.AreEqual(0, b.data[0]);
            Assert.AreEqual(1, b.data[1]);

            b = new BigSet(new List<int>());
            b.SetFalse(1);
            Assert.AreEqual(0, b.data[0]);

            b = new BigSet(new List<int>() {1, 2});
            b.SetFalse(1);
            b.SetFalse(2);
            Assert.AreEqual(0, b.data[0]);
        }

        [Test]
        public void BigSet_NextTrue()
        {
            var b = new BigSet(new List<int>() {1, 2});
            var next = b.GetNextTrue();
            Assert.AreEqual(1, next);

            b = new BigSet(new List<int>() {7, 12});
            next = b.GetNextTrue();
            Assert.AreEqual(7, next);

            b = new BigSet(new List<int>() {64});
            next = b.GetNextTrue();
            Assert.AreEqual(64, next);

            b = new BigSet(new List<int>() {65});
            next = b.GetNextTrue();
            Assert.AreEqual(65, next);

            b = new BigSet(new List<int>() { });
            next = b.GetNextTrue();
            Assert.AreEqual(-1, next);
        }

        [Test]
        public void BigSet_Equal()
        {

        }

        [Test]
        public void BigSet_Intersect()
        {
            BigSet b1;
            BigSet b2;

            b1 = new BigSet(new List<int>() { });
            b2 = new BigSet(new List<int>() { });
            b1.Intersect(b2);
            Assert.AreEqual(b1.data[0], b2.data[0]);

            b1 = new BigSet(new List<int>() {5});
            b2 = new BigSet(new List<int>() {2, 5, 7});
            b2.Intersect(b1);
            Assert.True(b1.Equals(b2));

            b1 = new BigSet(new List<int>() {3, 5});
            b2 = new BigSet(new List<int>() {2, 5, 7});
            b2.Intersect(b1);
            Assert.True(b2.Equals(new BigSet(new List<int>() {5})));

        }
    }
}
