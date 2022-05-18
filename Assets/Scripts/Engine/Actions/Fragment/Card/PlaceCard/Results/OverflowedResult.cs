using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PlaceCard
{
    public record OverflowedResult(
        [NotNull] PlaceCardAction Action,
        [NotNull] [ItemNotNull] ValueList<Interrupt<PlaceCardReplaceEffect>> ReplaceInterrupts
    ) : PlaceCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
