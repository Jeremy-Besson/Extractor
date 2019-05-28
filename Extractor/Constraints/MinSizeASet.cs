namespace Extractor.Constraints
{
    public class MinSizeASet : AMConstraint
    {
        public MinSizeASet(ulong minSize)
        {
            SetConstraint((sp, data) =>
            {
                return sp.GetASet().NumberOfPresent(minSize) >= minSize;
            });
        }
    }
}