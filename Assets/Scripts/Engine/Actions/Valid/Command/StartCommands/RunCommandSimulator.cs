using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.StartCommands
{
    public class StartCommandsSimulator
    {
        public StartCommandsSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] StartCommandsResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private StartCommandsResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            return Result.Process.Timeline.Simulate(Context, Initial);
        }
    }
}
