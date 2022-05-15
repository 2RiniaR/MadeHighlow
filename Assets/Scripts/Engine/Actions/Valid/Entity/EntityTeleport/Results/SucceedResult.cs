using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
{
    public record SucceedResult(
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityTeleportEffect>> Interrupts,
        [NotNull] Fragment.PositionEntity.SucceedResult PositionEntityResult
    ) : EntityTeleportResult
    {
        public override World Simulate(World world)
        {
            return PositionEntityResult.Simulate(world);
        }
    }
}
