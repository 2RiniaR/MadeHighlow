using JetBrains.Annotations;

namespace RineaR.MadeHighlow
{
    public record FailedRunCommandResult([NotNull] Command Command, FailedRunCommandReason Reason) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
