using System;
using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;

namespace Extractor.Tests
{
    public class BigSetTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BigSet_Create()
        {
            BigSet b;

            b = new BigSet(new List<int>());
            Assert.AreEqual(1, b.data.Count);

            var ind = 1;
            b = new BigSet(new List<int>(){ind});
            Assert.AreEqual(1, b.data.Count);
            Assert.AreEqual(System.Numerics.BigInteger.Pow(2, ind - 1), new BigInteger(b.data[0]));

            ind = 64;
            b = new BigSet(new List<int>() { ind });
            Assert.AreEqual(1, b.data.Count);
            Assert.AreEqual(BigInteger.Pow(2, ind - 1), new BigInteger(b.data[0]));

            ind = 65;
            b = new BigSet(new List<int>() { ind });
            Assert.AreEqual(2, b.data.Count);
            Assert.AreEqual(new BigInteger(0), new BigInteger(b.data[0]));
            Assert.AreEqual(new BigInteger(1), new BigInteger(b.data[1]));
        }

        [Test]
        public void BigSet_SetFalse()
        {
            BigSet b;
            int ind;

            b = new BigSet(new List<int>());
            b.SetFalse(1);
            Assert.AreEqual(0, b.data[0]);

            b = new BigSet(new List<int>() { 1, 2 });
            b.SetFalse(1);
            b.SetFalse(2);
            Assert.AreEqual(0, b.data[0]);

            b = new BigSet(new List<int>(){64});
            b.SetFalse(64);
            Assert.AreEqual(0, b.data[0]);

            b = new BigSet(new List<int>() { 65 });
            b.SetFalse(65);
            Assert.AreEqual(0, b.data[0]);
            Assert.AreEqual(0, b.data[1]);
        }

        [Test]
        public void BigSet_SetTrue()
        {
            BigSet b;
            int ind;

            b = new BigSet(new List<int>() { 1 });
            b.SetTrue(1);
            Assert.AreEqual(1, b.data[0]);
            b.SetTrue(1);
            Assert.AreEqual(1, b.data[0]);

            b = new BigSet(new List<int>() { 1 });
            b.SetTrue(2);
            Assert.AreEqual(3, b.data[0]);

            ind = 1;
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
            Assert.AreEqual(System.Numerics.BigInteger.Pow(2, ind - 1), new BigInteger(b.data[0]));

            ind = 64;
            b = new BigSet(new List<int>());
            b.SetTrue(ind);
            Assert.AreEqual(1, b.data.Count);
            Assert.AreEqual(System.Numerics.BigInteger.Pow(2, ind - 1), new BigInteger(b.data[0]));
        }

        [Test]
        public void BigSet_NextTrue()
        {
            var b = new BigSet(new List<int>() { 1, 2 });
            var next = b.GetNextTrue();
            Assert.AreEqual(1, next);

            b = new BigSet(new List<int>() { 7, 12 });
            next = b.GetNextTrue();
            Assert.AreEqual(7, next);

            b = new BigSet(new List<int>() { 64 });
            next = b.GetNextTrue();
            Assert.AreEqual(64, next);

            b = new BigSet(new List<int>() { 65 });
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

            b1 = new BigSet(new List<int>() { 5 });
            b2 = new BigSet(new List<int>() { 2, 5, 7 });
            b2.Intersect(b1);
            Assert.True(b1.Equals(b2));

            b1 = new BigSet(new List<int>() { 3, 5 });
            b2 = new BigSet(new List<int>() { 2, 5, 7 });
            b2.Intersect(b1);
            Assert.True(b2.Equals(new BigSet(new List<int>() { 5 })));

        }

    }
}