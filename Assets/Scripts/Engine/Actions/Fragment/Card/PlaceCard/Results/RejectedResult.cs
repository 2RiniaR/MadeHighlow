using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record RejectedResult(
        [NotNull] PlaceCardAction Action,
        [CanBeNull] [ItemNotNull] ValueList<Interrupt<CardReplacement>> ReplacementInterrupts,
        [NotNull] PlaceCardProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PlaceCardRejection>> RejectionInterrupts,
        [NotNull] ComponentID RejectedID
    ) : PlaceCardResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
