using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
{
    public record RejectedResult(
        [NotNull] EntityID TargetID,
        [NotNull] Position3D Destination,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityTeleportEffect>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : EntityTeleportResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
