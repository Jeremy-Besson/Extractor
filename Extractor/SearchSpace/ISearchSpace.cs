using System.Collections.Generic;

namespace Extractor
{
    public interface ISearchSpace
    {
        IBigSet GetASet();
        IBigSet GetBSet();
        Stack<int> GetYesB();
        List<int> GetShouldBePresentB();
        void SetSearchSpace(IBigSet v1, IBigSet v2, Stack<int> yesB = null);
        int GetNextToEnumerateB();
        IEnumerable<ISearchSpace> SplitB(int indexB);
        List<string> Print();
        void Init();
    }
}