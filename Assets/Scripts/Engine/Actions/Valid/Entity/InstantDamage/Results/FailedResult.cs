using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.InstantDamage
{
    public record FailedResult([NotNull] InstantDamageAction Action, FailedReason Reason) : InstantDamageResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
