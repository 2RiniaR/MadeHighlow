using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.Valid.StartCommands
{
    public record StartCommandsResult([NotNull] StartCommandsAction Action, [NotNull] Process Process) : ValidResult
    {
        public override World Simulate(World world)
        {
            return Process.Timeline.Simulate(world);
        }
    }
}
