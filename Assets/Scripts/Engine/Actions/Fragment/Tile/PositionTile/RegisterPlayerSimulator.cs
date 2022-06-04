using JetBrains.Annotations;

namespace RineaR.MadeHighlow.Actions.PositionTile
{
    public class PositionTileSimulator
    {
        public PositionTileSimulator(
            [NotNull] ISimulationContext context,
            [NotNull] World initial,
            [NotNull] PositionTileResult result
        )
        {
            Context = context;
            Initial = initial;
            Result = result;
        }

        [NotNull] private ISimulationContext Context { get; }
        [NotNull] private World Initial { get; }
        [NotNull] private PositionTileResult Result { get; }

        [NotNull]
        public World Simulate()
        {
            if (Result is SucceedResult succeedResult)
            {
                return Context.Modifier.UpdateTile(Initial, succeedResult.Positioned);
            }

            return Initial;
        }
    }
}
