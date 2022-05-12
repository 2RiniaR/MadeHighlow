using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions
{
    public interface IReferenceEffector
    {
        [NotNull]
        public World OnReferenced([NotNull] World world);
    }
}
