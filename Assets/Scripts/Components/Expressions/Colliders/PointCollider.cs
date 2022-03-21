using JetBrains.Annotations;
using RineaR.MadeHighlow.Engine.Subjects.Geometry;

namespace RineaR.MadeHighlow.Components.Expressions.Colliders
{
    public record PointCollider
    (
        [NotNull] Vector3D Vector3D
    );
}