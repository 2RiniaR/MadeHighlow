using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record OverflowedSupplyCardResult(
        [NotNull] PlayerID TargetPlayerID,
        [NotNull] Card SupplyCard
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
