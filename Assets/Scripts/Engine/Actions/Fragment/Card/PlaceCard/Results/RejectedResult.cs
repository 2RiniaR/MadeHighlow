using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record RejectedResult(
        [NotNull] Action Action,
        [CanBeNull] [ItemNotNull] ValueList<Interrupt<CardReplacement>> ReplacementInterrupts,
        [NotNull] Process Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : Result;
}
