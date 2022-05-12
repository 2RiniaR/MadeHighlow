using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record OverflowedResult([NotNull] PlayerID TargetPlayerID, [NotNull] Card SupplyCard) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
