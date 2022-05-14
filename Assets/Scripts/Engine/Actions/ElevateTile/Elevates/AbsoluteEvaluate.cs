using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ElevateTile
{
    public record AbsoluteElevate([NotNull] Elevation Elevation) : Elevate
    {
        public override Elevation Caused(Elevation original)
        {
            return Elevation;
        }
    }
}
