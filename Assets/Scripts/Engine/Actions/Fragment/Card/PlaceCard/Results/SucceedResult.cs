using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record SucceedResult(
        [NotNull] Action Action,
        [CanBeNull] [ItemNotNull] ValueList<Interrupt<CardReplacement>> ReplacementInterrupts,
        [NotNull] Process Process
    ) : Result;
}
