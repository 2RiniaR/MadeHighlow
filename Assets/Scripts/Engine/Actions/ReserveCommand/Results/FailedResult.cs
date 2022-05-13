using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.ReserveCommand
{
    public record FailedResult([NotNull] Command Command, FailedReason Reason) : ReserveCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
