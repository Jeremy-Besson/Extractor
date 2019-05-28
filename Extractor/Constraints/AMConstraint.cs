using System;

namespace Extractor.Constraints
{
    public class AMConstraint : IConstraint
    {
        private Func<ISearchSpace, Data, bool> Constraint { get; set; }

        protected void SetConstraint(Func<ISearchSpace, Data, bool> constraint)
        {
            Constraint = constraint;
        }

        public bool Satisfy(ISearchSpace searchSpace, Data data)
        {
            return Constraint(searchSpace, data);
        }
    }
}