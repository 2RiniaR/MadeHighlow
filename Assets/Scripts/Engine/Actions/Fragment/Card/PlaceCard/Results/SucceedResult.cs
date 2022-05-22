using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record SucceedResult(
        [NotNull] PlaceCardAction Action,
        [CanBeNull] [ItemNotNull] ValueList<Interrupt<CardReplacement>> ReplacementInterrupts,
        [NotNull] PlaceCardProcess Process,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PlaceCardRejection>> RejectionInterrupts
    ) : PlaceCardResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
