using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.ElevateTile
{
    public abstract record Elevate
    {
        [NotNull]
        public abstract Elevation Caused([NotNull] Elevation original);
    }
}
