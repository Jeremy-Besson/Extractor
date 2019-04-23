using System;
using System.Collections.Generic;

namespace Extractor
{
    public interface IBigSet
    {
        void SetValues(List<int> trueIndexs);
        void SetFalse(int indexB);
        void SetTrue(int trueIndex);
        int GetNextTrue();
        void Intersect(IBigSet col);
        bool Equals(Object obj);
        void Print();
        string ToString();
        int GetNumberA();
    }
}