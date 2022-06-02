using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public sealed record SucceedResult(
        [NotNull] InstantDeathAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDeathRejection>> RejectionInterrupts
    ) : InstantDeathResult;
}
