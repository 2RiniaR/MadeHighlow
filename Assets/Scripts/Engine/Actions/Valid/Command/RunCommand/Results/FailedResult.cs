using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public record FailedResult([NotNull] RunCommandAction Action, FailedReason Reason) : RunCommandResult
    {
        public override World Simulate(World world)
        {
            return world;
        }
    }
}
