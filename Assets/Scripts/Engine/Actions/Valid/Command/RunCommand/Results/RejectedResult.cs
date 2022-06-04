using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record RejectedResult(
        [NotNull] Action Action,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt> RejectionInterrupts,
        [CanBeNull] ComponentID RejectedID
    ) : Result;
}
