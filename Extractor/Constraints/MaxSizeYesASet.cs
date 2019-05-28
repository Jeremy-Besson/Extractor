namespace Extractor.Constraints
{
    public class MaxSizeYesASet: AMConstraint
    {

        public MaxSizeYesASet(ulong minSize)
        {
            SetConstraint((sp, data) =>
            {
                return (ulong)sp.GetYesB().Count <= minSize;
            });
        }
    }
}