using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public sealed record EntityCondition(
        [CanBeNull] Position2D Position2D = null,
        [CanBeNull] Position3D Position3D = null
    );
}
