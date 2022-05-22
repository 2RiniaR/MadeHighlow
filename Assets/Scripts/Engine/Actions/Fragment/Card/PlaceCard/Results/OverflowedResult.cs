using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record OverflowedResult(
        [NotNull] PlaceCardAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<CardReplacement>> ReplacementInterrupts
    ) : PlaceCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
