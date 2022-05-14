using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.RunCommand;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public record StartCommandsResult(
        [NotNull] [ItemNotNull] ValueList<ReactedResult<RunCommandResult>> RunCommandResults
    ) : Result
    {
        public override World Simulate(World world)
        {
            return RunCommandResults.Aggregate(world, (current, result) => result.Simulate(current));
        }
    }
}
