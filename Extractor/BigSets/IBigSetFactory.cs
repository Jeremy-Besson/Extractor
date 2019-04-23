using System.Collections.Generic;

namespace Extractor.BigSets
{
    public interface IBigSetFactory
    {
        IBigSet Create(List<int> indexes);
        IBigSet Clone(IBigSet bigSet);
    }
}
