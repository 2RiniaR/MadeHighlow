using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.CreateTile
{
    public class CreateTileSimulator
    {
        public CreateTileSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] CreateTileResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private CreateTileResult Result { get; }

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
