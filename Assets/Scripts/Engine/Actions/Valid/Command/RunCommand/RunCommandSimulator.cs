using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.RunCommand
{
    public class RunCommandSimulator
    {
        public RunCommandSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] RunCommandResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private RunCommandResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                return succeedResult.Process.Timeline.Simulate(Context, Initial);
            }

            return Initial;
        }
    }
}
