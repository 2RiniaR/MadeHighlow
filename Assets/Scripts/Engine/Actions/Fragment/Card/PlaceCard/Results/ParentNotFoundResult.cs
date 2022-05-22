using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PlaceCard
{
    public record ParentNotFoundResult([NotNull] PlaceCardAction Action) : PlaceCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
