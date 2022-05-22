using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDeath
{
    public record FailedResult([NotNull] InstantDeathAction Action, FailedReason Reason) : InstantDeathResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
