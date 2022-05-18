using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.SupplyCard
{
    public record DestroyedResult([NotNull] SupplyCardAction Action, [NotNull] Process Process) : SupplyCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
