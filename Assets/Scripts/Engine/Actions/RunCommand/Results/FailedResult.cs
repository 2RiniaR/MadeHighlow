using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record FailedResult([NotNull] Command Command, FailedReason Reason) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
