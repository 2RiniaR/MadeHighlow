using JetBrains.Annotations;
using RineaR.MadeHighlow.Actions.Valid.RunCommand;

namespace RineaR.MadeHighlow.Actions.Valid.StartCommands
{
    public record StartCommandsResult(
        [NotNull] StartCommandsAction Action,
        [NotNull] [ItemNotNull] ValueList<ReactedResult<RunCommandResult>> RunCommandEvents
    ) : ValidResult
    {
        public override World Simulate(World world)
        {
            return RunCommandEvents.Aggregate(world, (current, result) => result.Simulate(current));
        }
    }
}
