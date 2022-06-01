using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionEntity
{
    public record PositionEntityAction([NotNull] EntityID TargetID, [NotNull] Position3D Destination);
}
