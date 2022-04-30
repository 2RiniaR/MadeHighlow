using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.Expressions.Colliders
{
    public record PointCollider
    (
        [NotNull] Vector3D Vector3D
    );
}