using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Extractor.Constraints
{
    class ClosedBSet:AMConstraint
    {
        public ClosedBSet()
        {
            SetConstraint((sp, data) =>
            {
                return sp.GetShouldBePresentB().All(
                x =>
                {
                    return sp.GetASet().IntersectNotNull(data.getB(x));
                });
            });
        }
    }
}
