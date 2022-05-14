using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.RunCommand;

namespace RineaR.MadeHighlow.Actions.Valid.StartCommands
{
    public record StartCommandsResult(
        [NotNull] [ItemNotNull] ValueList<ReactedResult<RunCommandResult>> RunCommandResults
    ) : ValidResult
    {
        public override World Simulate(World world)
        {
            return RunCommandResults.Aggregate(world, (current, result) => result.Simulate(current));
        }
    }
}
