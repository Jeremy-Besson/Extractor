using System.Collections.Generic;
using System.Text;

namespace Extractor.Constraints
{
    interface IConstraint
    {
        bool Satisfy(ISearchSpace searchSpace, Data data);
    }
}
