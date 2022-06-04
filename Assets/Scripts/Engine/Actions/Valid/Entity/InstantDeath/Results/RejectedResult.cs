using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public record RejectedResult(
        [NotNull] Action Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : Result;
}
