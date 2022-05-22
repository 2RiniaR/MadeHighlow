using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.SupplyCard
{
    public record DestroyedResult(
        [NotNull] SupplyCardAction Action,
        [NotNull] SupplyCardProcess Process
    ) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
