namespace Extractor.Constraints
{
    public class MinSizeYesASet: AMConstraint
    {
        public MinSizeYesASet(ulong minSize)
        {
            SetConstraint((sp, data) =>
            {
                return (ulong)sp.GetYesB().Count >= minSize;
            });
        }
    }
}