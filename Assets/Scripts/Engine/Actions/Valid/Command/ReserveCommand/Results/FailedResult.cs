using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record FailedResult([NotNull] ReserveCommandAction Action, FailedReason Reason) : ReserveCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
