using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Components.Expressions.Colliders
{
    public record AreaCollider
    (
        [NotNull] Vector3D Min,
        [NotNull] Vector3D Max
    );
}