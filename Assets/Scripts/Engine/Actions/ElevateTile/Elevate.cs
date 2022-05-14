using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public abstract record Elevate
    {
        [NotNull]
        public abstract Elevation Caused([NotNull] Elevation original);
    }
}
