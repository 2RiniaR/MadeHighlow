using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public record RejectedResult(
        [NotNull] InstantDeathAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<InstantDeathRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : InstantDeathResult;
}
