using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.GenerateTile
{
    public class GenerateTileSimulator
    {
        public GenerateTileSimulator(
            [NotNull] SimulationContext context,
            [NotNull] World initial,
            [NotNull] GenerateTileResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private SimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private GenerateTileResult Result { get; }

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
