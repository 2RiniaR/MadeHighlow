using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record RejectedSupplyCardResult
        ([NotNull] Card Card, [NotNull] ComponentID RejectedComponentID) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
