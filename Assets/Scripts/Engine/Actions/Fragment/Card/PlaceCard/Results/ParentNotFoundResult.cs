using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PlaceCard
{
    public record ParentNotFoundResult([NotNull] PlaceCardAction Action) : PlaceCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
