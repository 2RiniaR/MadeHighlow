using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record FailedSupplyCardResult(
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
