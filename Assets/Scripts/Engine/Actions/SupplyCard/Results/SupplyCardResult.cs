using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public abstract record SupplyCardResult : Result
    {
        [NotNull] public new static SupplyCardResult Empty => new EmptyImpl();

        private record EmptyImpl : SupplyCardResult
        {
            public override World Simulate(in World world)
            {
                return world;
            }
        }
    }
}