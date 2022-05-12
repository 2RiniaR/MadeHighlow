using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.AddComponent;
using RineaR.MadeHighlow.Actions.SupplyCard.RegisterCard;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record FailedResult(
        [NotNull] Card Card,
        [NotNull] RegisterCardResult RegisterCardResult,
        [NotNull] [ItemNotNull] ValueList<AddComponentResult> AddComponentResults
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
