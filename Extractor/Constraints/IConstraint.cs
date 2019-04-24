using System.Collections.Generic;
using System.Text;

namespace Extractor.Constraints
{
    interface IConstraint
    {
        bool Satisfy(SearchSpace searchSpace, Data data);
    }
}
