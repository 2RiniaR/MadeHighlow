using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Fragment.PutCard
{
    public record OverflowedResult([NotNull] Card Target) : PutCardResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
