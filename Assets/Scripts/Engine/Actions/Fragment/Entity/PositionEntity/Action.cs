using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionEntity
{
    public record Action([NotNull] EntityID TargetID, [NotNull] Position3D Destination);
}
