using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityTeleport
{
    public record SucceedResult(
        [NotNull] EntityTeleportAction Action,
        [NotNull] EntityTeleportProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityTeleportRejection>> Interrupts
    ) : EntityTeleportResult;
}
