namespace Extractor.Constraints
{
    public class MinSizeBSet : AMConstraint
    {
        private int minSize { get; set; }

        public MinSizeBSet(int minSize)
        {
            this.minSize = minSize;
            SetConstraint((sp, data) =>
            {
                //return sp.SizeB > minSize;
                return true;
            });
        }
    }
}