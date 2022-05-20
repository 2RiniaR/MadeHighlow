using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.EntityTeleport
{
    public record SucceedResult(
        [NotNull] EntityTeleportAction Action,
        [NotNull] EntityTeleportProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityTeleportRejection>> Interrupts
    ) : EntityTeleportResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
