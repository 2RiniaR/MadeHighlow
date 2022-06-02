using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record SucceedResult(
        [NotNull] EntityFlyAction Action,
        [NotNull] EntityFlyProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<EntityFlyRejection>> RejectionInterrupts
    ) : EntityFlyResult;
}
