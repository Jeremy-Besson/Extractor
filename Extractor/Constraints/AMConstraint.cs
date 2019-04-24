using System;

namespace Extractor.Constraints
{
    public class AMConstraint : IConstraint
    {
        private Func<SearchSpace, Data, bool> Constraint { get; set; }

        protected void SetConstraint(Func<SearchSpace, Data, bool> constraint)
        {
            Constraint = constraint;
        }

        public bool Satisfy(SearchSpace searchSpace, Data data)
        {
            return Constraint(searchSpace, data);
        }
    }
}