using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.InstantHeal
{
    public record FailedResult([NotNull] InstantHealAction Action, FailedReason Reason) : InstantHealResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
