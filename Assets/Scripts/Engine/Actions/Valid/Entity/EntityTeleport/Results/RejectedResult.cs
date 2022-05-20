using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
{
    public record RejectedResult(
        [NotNull] EntityTeleportAction Action,
        [NotNull] EntityTeleportProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityTeleportRejection>> Interrupts,
        [NotNull] ComponentID RejectedID
    ) : EntityTeleportResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
