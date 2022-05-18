using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Fragment.PlaceCard;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public record PlaceCardFailedResult(
        [NotNull] SupplyCardAction Action,
        [NotNull] PlaceCardResult Failed
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
