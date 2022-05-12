using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record RejectedResult([NotNull] Card Card, [NotNull] ComponentID RejectedComponentID) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
