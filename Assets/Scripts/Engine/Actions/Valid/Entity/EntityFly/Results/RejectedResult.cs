using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.EntityFly
{
    public record RejectedResult(
        [NotNull] Action Action,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : Result;
}
