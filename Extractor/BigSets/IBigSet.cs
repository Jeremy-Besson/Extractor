using System;
using System.Collections.Generic;

namespace Extractor
{
    public interface IBigSet
    {
        int GetMaxIndexA();
        void SetValues(List<int> trueIndexs);
        void SetFalse(int indexB);
        void SetTrue(int indexB);
        int GetNextTrue();
        void Intersect(IBigSet col);
        ulong NumberOfPresent(ulong? minSize = null);
        bool IntersectNotNull(IBigSet bigSet);
        bool Equals(Object obj);
        void Print();
        string ToString();
        
    }
}