using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RunCommand;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public record SucceedResult
        ([NotNull] [ItemNotNull] ValueList<RunCommandResult> RunCommandResults) : StartCommandsResult
    {
        public override World Simulate(World world)
        {
            return RunCommandResults.Aggregate(world, (current, result) => result.Simulate(current));
        }
    }
}
