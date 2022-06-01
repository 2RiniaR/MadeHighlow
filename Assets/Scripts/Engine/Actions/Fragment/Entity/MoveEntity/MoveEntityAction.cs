using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public record MoveEntityAction([NotNull] EntityID TargetID, [NotNull] Direction3D Direction);
}
