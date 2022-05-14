using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.ReserveCommand
{
    public record FailedResult([NotNull] Command Command, FailedReason Reason) : ReserveCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
