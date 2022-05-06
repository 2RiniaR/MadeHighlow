using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record OverflowedSupplyCardResult(
        [NotNull] in PlayerID TargetPlayerID,
        [NotNull] in Card SupplyCard
    ) : SupplyCardResult
    {
        public override World Simulate(in World world)
        {
            return world;
        }
    }
}