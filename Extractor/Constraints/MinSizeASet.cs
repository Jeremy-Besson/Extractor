namespace Extractor.Constraints
{
    public class MinSizeASet: AMConstraint
    {
        private int minSize { get; set; }

        public MinSizeASet(int minSize)
        {
            this.minSize = minSize;
            SetConstraint((sp, data) =>
            {
                //return sp.SizeA > minSize;
                return true;
            });
        }
    }
}