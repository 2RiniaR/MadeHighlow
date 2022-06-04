using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.MoveEntity
{
    public record Action([NotNull] EntityID TargetID, [NotNull] Direction3D Direction);
}
