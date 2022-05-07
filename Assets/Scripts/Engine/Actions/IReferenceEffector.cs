using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public interface IReferenceEffector
    {
        [NotNull]
        public World OnReferenced([NotNull] World world);
    }
}
