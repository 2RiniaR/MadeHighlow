using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public record StartCommandsResult(
        [NotNull] StartCommandsAction Action,
        [NotNull] StartCommandsProcess Process
    ) : IValidResult
    {
        public World Simulate(ISimulationContext context, World world)
        {
            return new StartCommandsSimulator(context, world, this).Simulate();
        }
    }
}
