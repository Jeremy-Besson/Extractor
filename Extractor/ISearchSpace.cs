using System.Collections.Generic;

namespace Extractor
{
    public interface ISearchSpace
    {
        void Copy(IBigSet v1, IBigSet v2, List<int> yesB = null);
        int GetNextToEnumerateB();
        IEnumerable<ISearchSpace> SplitB(int indexB);
        List<string> Print();
        void Init();
    }
}