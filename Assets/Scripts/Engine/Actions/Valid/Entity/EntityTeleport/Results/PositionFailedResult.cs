using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PositionEntity;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
{
    public record PositionFailedResult(
        [NotNull] EntityID TargetID,
        [NotNull] Position3D Destination,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityTeleportEffect>> Interrupts,
        [NotNull] PositionEntityResult FailedResult
    ) : EntityTeleportResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
